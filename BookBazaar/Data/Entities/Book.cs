using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class Book
{
    [HiddenInput(DisplayValue = false)]
    [Display(Name = "ID")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Будь ласка, введіть назву книги")]
    [Display(Name = "Назва")]
    public string Name { get; set; }

    [DataType(DataType.MultilineText)]
    [Required(ErrorMessage = "Будь ласка, введіть опис")]
    [Display(Name = "Будь ласка, введіть опис")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Будь ласка, введіть жанр книги")]
    [Display(Name = "Категорія")]
    public string Category { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Required(ErrorMessage = "Будь ласка, введіть ціну")]
    [Range(0.001, double.MaxValue, ErrorMessage = "Будь ласка, введіть позитивне число")]
    [Display(Name = "Ціна (грн)")]
    public decimal Price { get; set; }
}
