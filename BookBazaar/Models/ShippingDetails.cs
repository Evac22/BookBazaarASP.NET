using System.ComponentModel.DataAnnotations;

namespace BookBazaar.Models
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите Ваше имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите Вашу фамилию")]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Укажите адрес доставки")]
        [Display(Name = "Адреса доставки")]
        public string Address { get; set; }

        
    [Required(ErrorMessage = "Email")]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Телефон")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}
