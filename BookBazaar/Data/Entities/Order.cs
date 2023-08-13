using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class Order
{
    public Guid Id { get; set; }

    [Display(Name = "Ім'я")]
    public string FirstName { get; set; }

    [Display(Name = "Прізвище")]
    public string SecondName { get; set; }

    [Display(Name = "Адреса доставки")]
    public string Address { get; set; }

    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [Display(Name = "Телефон")]
    public string Phone { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Усього (грн)")]
    public decimal TotalPrice { get; set; }

    public ICollection<OrderedBook> OrderedBook { get; set; }
}
