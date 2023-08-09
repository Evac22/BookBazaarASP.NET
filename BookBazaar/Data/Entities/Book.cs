using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Book
    {
        [HiddenInput(DisplayValue =false)]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название книги")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        [Display(Name = "Пожалуйста, введите описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите жанр")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите цену")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное число")]
        [Display(Name = "Цена (грн)")]
        public decimal Price { get; set; }
    }
}
