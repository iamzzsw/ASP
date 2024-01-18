using Domain.Abstract;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WebUI.Controllers;
using System.Linq;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Can_Paginate()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{BookId = 1, Title = "Book1"},
                new Book{BookId = 2, Title = "Book2"},
                new Book{BookId = 3, Title = "Book3"},
                new Book{BookId = 4, Title = "Book4"},
                new Book{BookId = 5, Title = "Book5"}
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            IEnumerable<Book> result = (IEnumerable<Book>)controller.List(null, 2).Model;

            List<Book> books = result.ToList();
            Assert.IsTrue(books.Count == 2);
            Assert.AreEqual(books[0].Title, "Book4");
            Assert.AreEqual(books[1].Title, "Book5");
        }
    }
}