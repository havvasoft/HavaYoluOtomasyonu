using HavaYoluOtomasyonu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;      // YENİ EKLENDİ (Task için)
using System.Collections.Generic;  // YENİ EKLENDİ (List için)
using System;                      // YENİ EKLENDİ (DateTime için)


namespace HavaYoluOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public HomeController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Havalimanlari = _context.Airports.ToList();

            ViewBag.YaklasanUcuslar = _context.Flights
                .Include(f => f.Routes)
                    .ThenInclude(r => r.DepartureAirport)
                .Include(f => f.Routes)
                    .ThenInclude(r => r.ArrivalAirport)
                .Where(f => f.DepartureTime > DateTime.Now)
                .OrderBy(f => f.DepartureTime)
                .Take(6)
                .ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password, string UserType)
        {
            // 🚨 1. EN TEPEDEKİ ORTAK KİLİT: Giren kim olursa olsun (Müşteri, Admin, Staff fark etmez) e-posta veya şifre boşsa kapıdan çevir!
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ViewBag.HataMesaji = "Lütfen e-posta adresinizi ve şifrenizi eksiksiz giriniz!";
                return View();
            }

            // 2. EĞER GİREN KİŞİ ADMİN VEYA PERSONELSE (İkisi de Staff tablosunda)
            if (UserType == "Admin" || UserType == "Staff")
            {
                var personel = _context.Staff
                    .Include(s => s.Role)
                    .FirstOrDefault(s => s.Email == Email && s.Password == Password);

                if (personel != null)
                {
                    string rolAdi = personel.Role?.RoleName ?? "BosVeyaYok";

                    // Ekstra Güvenlik: Admin seçip giren kişinin veritabanındaki rolü gerçekten Admin mi?
                    if (UserType == "Admin" && rolAdi != "Admin")
                    {
                        ViewBag.HataMesaji = $"Şifreniz doğru ama yetkiniz uyuşmuyor! Veritabanındaki Rolünüz: '{rolAdi}'. Biz 'Admin' bekliyoruz.";
                        return View();
                    }

                    var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, personel.StaffId.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, rolAdi)
            };

                    var identity = new System.Security.Claims.ClaimsIdentity(claims, "AeroSysAuth");
                    await HttpContext.SignInAsync("AeroSysAuth", new System.Security.Claims.ClaimsPrincipal(identity));

                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ViewBag.HataMesaji = "Bu e-posta ve şifreye sahip bir personel veritabanında bulunamadı!";
                    return View();
                }
            }

            // 3. EĞER GİREN KİŞİ MÜŞTERİYSE (Passengers tablosuna bakacak)
            else if (UserType == "Customer")
            {
                var musteri = _context.Passengers.FirstOrDefault(m => m.Email == Email && m.Password == Password);

                if (musteri != null)
                {
                    var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, musteri.PassengerId.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Musteri")
            };

                    var identity = new System.Security.Claims.ClaimsIdentity(claims, "AeroSysAuth");
                    await HttpContext.SignInAsync("AeroSysAuth", new System.Security.Claims.ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Tickets"); // Müşteriyse kendi biletlerine at
                }
            }

            // Hiçbir şart sağlanmazsa (Örneğin şifre yanlışsa):
            ViewBag.HataMesaji = "Giriş Türü, E-posta veya Şifre hatalı!";
            return View();
        }
        public IActionResult Dashboard()
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("Musteri"))
            {
                return RedirectToAction("Login", "Home"); // Yetkisizse Login'e geri at
            }

            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}