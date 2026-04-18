using System.ComponentModel.DataAnnotations;

namespace laba1.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ФИО обязательно")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "ФИО")]
        public string? FullName { get; set; }

        [Display(Name = "Компания")]
        public string? Company { get; set; }

        [Required(ErrorMessage = "ИНН обязателен")]
        [RegularExpression(@"^\d{10}(\d{2})?$", ErrorMessage = "ИНН должен содержать 10 или 12 цифр")]
        [Display(Name = "ИНН")]
        public string? INN { get; set; }

        [Phone(ErrorMessage = "Некорректный телефон")]
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный email")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Активен")]
        public bool IsActive { get; set; }

        // Метод
        public int GetYearsWithUs()
        {
            return DateTime.Now.Year - RegistrationDate.Year;
        }
    }
}