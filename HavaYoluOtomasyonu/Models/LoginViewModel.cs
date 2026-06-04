using System.ComponentModel.DataAnnotations;

namespace HavaYoluOtomasyonu.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen bir rol seçiniz.")]
        public string Role { get; set; } = "Customer";

        [Required(ErrorMessage = "E-posta veya pasaport numarası giriniz.")]
        [Display(Name = "E-posta veya Pasaport")]
        public string? Email { get; set; }
    }
}
