
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Display(Name = "Адрес доставки")]
        public string Address { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Всего (грн)")]
        public string TotalPrice { get; set; }

        public ICollection<OrderedBook> OrderedBook { get; set; }
    }
}
