using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryInfoDataModelClassLibrary;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LibraryInfoWpfApp;
using NUnit.Framework;

namespace LibraryInfo.Test
{
    [TestFixture]
    class APITest
    {
        Mock<DbSet<Book>> mockBookDbSet;
        Mock<DbSet<CheckOutLog>> mockCheckOutLogDbSet;
        Book expectedBook;
        CheckOutLog expectedCheckOutLog;
        Mock<LibraryInfoEntities> mockLibraryInfoContext;

        [SetUp]
        public void TestInitialize()
        {
            mockBookDbSet = new Mock<DbSet<Book>>();
            mockCheckOutLogDbSet = new Mock<DbSet<CheckOutLog>>();

            expectedBook = new Book
            {
                BookID = 1,
                ISBN = "123456789",
                Title = "pro",
                AuthorID = 12,
                NumberPages = 500,
                Subject = "subject",
                Description = "description",
                Publisher = "publisher",
                Language = "language",
                NumberOfCopies = 3
            };

            expectedCheckOutLog = new CheckOutLog
            {
                CardholderID = 1,
                BookID = expectedBook.BookID,
                CheckOutDate = DateTime.Now
            };

            mockLibraryInfoContext = new Mock<LibraryInfoEntities>();
            mockLibraryInfoContext.Setup(m => m.Books).Returns(mockBookDbSet.Object);
            mockLibraryInfoContext.Setup(m => m.CheckOutLogs).Returns(mockCheckOutLogDbSet.Object);
        }

        [Test]
        public void API_FindBooks_Success()
        {
            //Arrange
            var expectedBooksList = new List<Book>()
            {
                expectedBook
            }.AsQueryable();

            var mockBookDbSet = new Mock<DbSet<Book>>();
            mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(expectedBooksList.Provider);
            mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(expectedBooksList.Expression);
            mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(expectedBooksList.ElementType);
            mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(expectedBooksList.GetEnumerator());

            //Act
            var actualBookList = API.FindBooks("C#");

            //Assert
            Assert.IsNotNull(actualBookList);
            Assert.IsTrue(actualBookList.Count == 1);
        }

        [Test]
        public void API_GetAvailableCopies_Success()
        {
            //Arrange
            var expectedBooksList = new List<Book>()
            {
                expectedBook
            }.AsQueryable();

            var mockBookDbSet = new Mock<DbSet<Book>>();
            mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(expectedBooksList.Provider);
            mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(expectedBooksList.Expression);
            mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(expectedBooksList.ElementType);
            mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(expectedBooksList.GetEnumerator());

            var expectedCheckOutLogsList = new List<CheckOutLog>()
            {
                expectedCheckOutLog
            }.AsQueryable();

            var mockCheckOutLogDbSet = new Mock<DbSet<CheckOutLog>>();
            mockCheckOutLogDbSet.As<IQueryable<CheckOutLog>>().Setup(m => m.Provider).Returns(expectedCheckOutLogsList.Provider);
            mockCheckOutLogDbSet.As<IQueryable<CheckOutLog>>().Setup(m => m.Expression).Returns(expectedCheckOutLogsList.Expression);
            mockCheckOutLogDbSet.As<IQueryable<CheckOutLog>>().Setup(m => m.ElementType).Returns(expectedCheckOutLogsList.ElementType);
            mockCheckOutLogDbSet.As<IQueryable<CheckOutLog>>().Setup(m => m.GetEnumerator()).Returns(expectedCheckOutLogsList.GetEnumerator());

            //Act
            var actualAvailableCopies = API.GetNumberAvailableBookCopies(expectedBook);

            //Assert
            Assert.IsTrue(actualAvailableCopies == 2);
        }
    }
}
