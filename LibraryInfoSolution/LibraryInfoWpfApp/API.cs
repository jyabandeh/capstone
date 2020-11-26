using LibraryInfoDataModelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryInfoWpfApp
{
    public class API
    {
        public static Book CreateBook(string isbn, string title, Author author, int numPages, string subject, string description, string publisher, string yearPublished, string language, int numCopies)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));            
            
            using (var db = new LibraryInfoEntities())
            {    
                Book b = new Book()
                {
                    AuthorID = author.ID,
                    //Author = author,
                    Description = description,
                    ISBN = isbn,
                    Language = language,
                    NumberOfCopies = numCopies,
                    NumberPages = numPages,
                    Publisher = publisher,
                    Subject = subject,
                    Title = title,
                    YearPublished = yearPublished
                };

                var foundAuthor = db.Authors.FirstOrDefault(a => a.Person.Firstname == author.Person.Firstname &&
                                               a.Person.Lastname == author.Person.Lastname);
                if (foundAuthor == null)
                {
                    b.Author = CreateAuthor(author.Person.Firstname, author.Person.Lastname, author.Bio);
                    b.AuthorID = b.Author.ID;
                }
                db.Books.Add(b);
                db.SaveChanges();
                return b;
            }
        }

        internal static Author CreateAuthor(string firstname, string lastname, string bio)
        {
            using (var db = new LibraryInfoEntities())
            {
                Person person = new Person()
                {
                    Firstname = firstname,
                    Lastname = lastname
                };
                db.People.Add(person);
                db.SaveChanges();

                Author author = new Author
                {
                    ID = person.PersonID,
                    Bio = bio
                };
                db.Authors.Add(author);
                db.SaveChanges();

                return author;
            }
        }

        static public List<Book> FindBooks(string searchText)
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Books.Include("Author.Person")
                               .Where(b => b.Title.Contains(searchText) ||
                                           b.Subject.Contains(searchText) ||
                                           b.ISBN.Contains(searchText) ||
                                           b.Author.Person.Lastname.Contains(searchText) ||
                                           b.Author.Person.Firstname.Contains(searchText))
                               .Distinct()
                               .ToList();
            }
        }

        static public Book GetBook(string isbn)
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Books.Include("Author.Person")
                                .Where(b => b.ISBN == isbn)
                                .SingleOrDefault();
            }
        }

        public static Author GetAuthor(int id)
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Authors.Include("Person")
                                 .Where(a => a.ID == id)
                                 .SingleOrDefault();
            }
        }

        static public int GetNumberAvailableBookCopies(Book book)
        {
            using (var db = new LibraryInfoEntities())
            {
                return book.NumberOfCopies - db.CheckOutLogs.Count(c => c.BookID == book.BookID);
            }
        }

        public static bool IsAccountOverdue(Cardholder cardholder)
        {
            using (var db = new LibraryInfoEntities())
            {
                var dueDate = DateTime.Now.AddDays(-30);
                return db.CheckOutLogs.Where(c => c.CardholderID == cardholder.ID && c.CheckOutDate <= dueDate)
                                      .Count() > 0;
            }
        }

        public static void CheckOutBook(Book book, Cardholder cardholder)
        {            
            using (var db = new LibraryInfoEntities())
            {
                CheckOutLog checkOutLog = new CheckOutLog
                {
                    BookID = book.BookID,
                    CardholderID = cardholder.ID,
                    CheckOutDate = DateTime.Now
                };
                db.CheckOutLogs.Add(checkOutLog);
                db.SaveChanges();
                MessageBox.Show($"{cardholder.LibraryCardID} has checked out {book.Title}");
            }
        }

        public static void CheckInBook(Book book, Cardholder cardholder)
        {
            using (var db = new LibraryInfoEntities())
            {
                CheckOutLog checkOutLog = db.CheckOutLogs.SingleOrDefault(c => c.BookID == book.BookID && c.CardholderID == cardholder.ID);
                if (checkOutLog != null)
                {
                    db.CheckOutLogs.Remove(checkOutLog);
                    db.SaveChanges();
                    MessageBox.Show($"{cardholder.LibraryCardID} has checked in {book.Title}");
                }
            }
        }

        public static Cardholder GetCardholder(string libraryCardID)
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Cardholders.SingleOrDefault(c => c.LibraryCardID == libraryCardID);
            }
        }

        public static List<Author> GetAuthors()
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Authors.Include("Person").OrderBy(a => a.Person.Lastname).ToList();
            }
        }

        public static bool IsValidLogin(string username, string password)
        {
            using (var db = new LibraryInfoEntities())
            {
                var foundLogin = db.Librarians.Where(a => a.UserID == username && a.Password == password).FirstOrDefault();
                if (foundLogin != null)
                    return true;
                else
                    return false;
            }
        }

        public static void AddBookCopies(Book book, int number)
        {
            using (var db = new LibraryInfoEntities())
            {
                book.NumberOfCopies += number;
                db.SaveChanges();
            }
        }

    }
}
