using BookBazaar.Models;
using Data.Entities;

namespace UnitTests
{
    public class CartTests
    {
        [Fact]
        public void AddItem_AddsNewLine_WhenBookNotInCart()
        {
            // Arrange
            var cart = new Cart();
            var book = new Book { Id = Guid.NewGuid(), Name = "Book 1", Price = 10 };
            var quantity = 2;

            // Act
            cart.AddItem(book, quantity);

            // Assert
            Assert.Single(cart.Lines);
            Assert.Equal(book, cart.Lines.First().Book);
            Assert.Equal(quantity, cart.Lines.First().Quantity);
        }

        [Fact]
        public void AddItem_IncreasesQuantity_WhenBookAlreadyInCart()
        {
            // Arrange
            var cart = new Cart();
            var book = new Book { Id = Guid.NewGuid(), Name = "Book 1", Price = 10 };
            var quantity1 = 2;
            var quantity2 = 3;

            //Act 
            cart.AddItem(book, quantity1);
            cart.AddItem(book, quantity2);

            // Assert
            Assert.Single(cart.Lines);
            Assert.Equal(book, cart.Lines.First().Book);
            Assert.Equal(quantity1 + quantity2, cart.Lines.First().Quantity);
        }

        [Fact]
        public void RemoveLine_RemovesLine_WhenFBookInCart()
        {
            var cart = new Cart();
            var book1 = new Book { Id = Guid.NewGuid(), Name = "Book 1", Price = 10 };
            var book2 = new Book { Id = Guid.NewGuid(), Name = "Book 2", Price = 20 };
            var quantity1 = 2;
            var quantity2 = 3;
            cart.AddItem(book1, quantity1);
            cart.AddItem(book2, quantity2);

            // Act
            cart.RemoveLine(book1);

            // Assert
            Assert.Single(cart.Lines);
            Assert.Equal(book2, cart.Lines.First().Book);
            Assert.Equal(quantity2, cart.Lines.First().Quantity);
        }

        [Fact]
        public void ComputeTotalValue_CalculatesCorrectly()
        {
            // Arrange
            var cart = new Cart();
            var book1 = new Book { Id = Guid.NewGuid(), Name = "Book 1", Price = 10 };
            var book2 = new Book { Id = Guid.NewGuid(), Name = "Book 2", Price = 20 };
            var quantity1 = 2;
            var quantity2 = 3;
            cart.AddItem(book1, quantity1);
            cart.AddItem(book2, quantity2);

            // Act
            var totalValue = cart.ComputeTotalValue();

            // Assert
            Assert.Equal(quantity1 * book1.Price + quantity2 * book2.Price, totalValue);
        }

        [Fact]
        public void Clear_RemovesAllLines()
        {
            // Arrange
            var cart = new Cart();
            var book1 = new Book { Id = Guid.NewGuid(), Name = "Book 1", Price = 10 };
            var book2 = new Book { Id = Guid.NewGuid(), Name = "Book 2", Price = 20 };
            var quantity1 = 2;
            var quantity2 = 3;
            cart.AddItem(book1, quantity1);
            cart.AddItem(book2, quantity2);

            // Act
            cart.Clear();

            // Assert
            Assert.Empty(cart.Lines);
        }

    }
}
