using Data.Entities;

namespace BookBazaar.Models;

public class CartLine
{
    public Book Book { get; set; }
    public int Quantity { get; set; }
}