using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HavaYoluOtomasyonu.Models.HavayoluOtomasyonDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
    "Server=.;Database=HavayoluOtomasyonDB;Trusted_Connection=True;TrustServerCertificate=True;"));

// Add services to the container.
builder.Services.AddControllersWithViews();
// Sisteme Cookie (Çerez) ile giriş yapma özelliğini ekliyoruz
builder.Services.AddAuthentication("AeroSysAuth").AddCookie("AeroSysAuth", options =>
{
    options.LoginPath = "/Home/Login"; // Giriş yapmayanları buraya yönlendir
    options.Cookie.Name = "AeroSys_Giris_Karti";
});
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
