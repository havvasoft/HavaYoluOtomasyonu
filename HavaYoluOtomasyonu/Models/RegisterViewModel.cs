using System.ComponentModel.DataAnnotations;

namespace HavaYoluOtomasyonu.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ad")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Soyad")]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "E-posta")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Pasaport No")]
        public string PassportNumber { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime DateOfBirth { get; set; }
    }
}
