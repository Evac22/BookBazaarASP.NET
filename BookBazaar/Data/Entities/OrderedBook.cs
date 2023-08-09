using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class OrderedBook
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
