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
        public static Book CreateBook(string isbn, string title, Author author, int numPages, string subject, string description,
                                      string publisher, string yearPublished, string language, int numCopies)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));            
            
            using (var db = new LibraryInfoEntities())
            {
                if (!IsBookInDB(isbn))
                {
                    Book b = new Book()
                    {
                        AuthorID = author.ID,
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

                    db.Books.Add(b);
                    db.SaveChanges();
                    return b;
                }
                else
                {
                    MessageBox.Show("A book with that ISBN already exists");
                    return null;
                }
            }
        }

        public static Book UpdateBook(Book book, string isbn, string title, Author author, int numPages, string subject, string description,
                                       string publisher, string yearPublished, string language, int numCopies)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            using (var db = new LibraryInfoEntities())
            {
                if (book.ISBN == isbn || !IsBookInDB(isbn))
                {
                    int numCopiesCheckedOut = book.NumberOfCopies - GetNumberAvailableBookCopies(book);
                    if (numCopiesCheckedOut <= numCopies)
                    {
                        book = db.Books.FirstOrDefault(b => b.BookID == book.BookID);
                        book.AuthorID = author.ID;
                        book.Description = description;
                        book.ISBN = isbn;
                        book.Language = language;
                        book.NumberOfCopies = numCopies;
                        book.NumberPages = numPages;
                        book.Publisher = publisher;
                        book.Subject = subject;
                        book.Title = title;
                        book.YearPublished = yearPublished;

                        db.SaveChanges();
                        return book;
                    }
                    else
                    {
                        MessageBox.Show($"There are currently {numCopiesCheckedOut} copies checked out. " +
                            $"New number of total copies cannot be less than this.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("A book with that ISBN already exists");
                    return null;
                }
            }
        }

        public static Author CreateAuthor(string firstname, string lastname, string bio)
        {
            using (var db = new LibraryInfoEntities())
            {
                var foundAuthor = db.Authors.FirstOrDefault(a => a.Person.Firstname == firstname &&
                                                                 a.Person.Lastname == lastname);
                var foundPerson = db.People.FirstOrDefault(p => p.Firstname == firstname &&
                                                                p.Lastname == lastname);

                if (foundAuthor == null)
                {
                    Person person;
                    if (foundPerson == null)
                    {
                        person = new Person()
                        {
                            Firstname = firstname,
                            Lastname = lastname
                        };
                        db.People.Add(person);
                        db.SaveChanges();
                    }
                    else
                    {
                        person = foundPerson;
                    }

                    Author author = new Author
                    {
                        ID = person.PersonID,
                        Bio = bio
                    };
                    db.Authors.Add(author);
                    db.SaveChanges();

                    return author;
                }

                else
                {
                    MessageBox.Show("An author with that name already exists.");
                    return null;
                }
            }
        }

        static public List<Book> FindBooks(string searchText)
        {
            using (var db = new LibraryInfoEntities())
            {
                var books = db.Books.Include("Author.Person")
                               .Where(b => b.Title.Contains(searchText) ||
                                           b.Subject.Contains(searchText) ||
                                           b.ISBN.Contains(searchText) ||
                                           b.Author.Person.Lastname.Contains(searchText) ||
                                           b.Author.Person.Firstname.Contains(searchText))
                               .Distinct()
                               .ToList();
                if (books.Count == 0)
                    MessageBox.Show("No books found matching search parameter.");
                return books;
            }
        }

        static public Book GetBook(string isbn)
        {
            using (var db = new LibraryInfoEntities())
            {
                var book = db.Books.Include("Author.Person")
                                   .Where(b => b.ISBN == isbn)
                                   .SingleOrDefault();
                if (book == null)
                    MessageBox.Show("Invalid ISBN.");
                return book;
            }
        }

        static public bool IsBookInDB(string isbn)
        {
            using (var db = new LibraryInfoEntities())
            {
                var book = db.Books.Where(b => b.ISBN == isbn)
                                   .SingleOrDefault();
                if (book == null)
                    return false;
                return true;
            }
        }

        public static Book GetBookByID(int id)
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Books.Include("Author.Person")
                               .Where(b => b.BookID == id)
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

        public static bool IsBookOverdue(CheckOutLog checkoutLog)
        {
            using (var db = new LibraryInfoEntities())
            {
                var dueDate = DateTime.Now.AddDays(-30);
                return checkoutLog.CheckOutDate <= dueDate;
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
                MessageBox.Show($"{cardholder.LibraryCardID} has checked out '{book.Title}'");
            }
        }

        public static void CheckInBook(Book book, Cardholder cardholder)
        {
            using (var db = new LibraryInfoEntities())
            {
                CheckOutLog checkOutLog = db.CheckOutLogs.FirstOrDefault(c => c.BookID == book.BookID && c.CardholderID == cardholder.ID);
                if (checkOutLog != null)
                {
                    db.CheckOutLogs.Remove(checkOutLog);
                    db.SaveChanges();
                    MessageBox.Show($"{cardholder.LibraryCardID} has checked in '{book.Title}'");
                }
                else
                {
                    MessageBox.Show($"{cardholder.LibraryCardID} and '{book.Title}' do not exist together in the checkout log.");
                }
            }
        }

        public static Cardholder GetCardholder(string libraryCardID)
        {
            using (var db = new LibraryInfoEntities())
            {
                var cardholder = db.Cardholders.SingleOrDefault(c => c.LibraryCardID == libraryCardID);
                if (cardholder == null)
                    MessageBox.Show("Invalid library card ID");
                return cardholder;
            }
        }

        public static List<Author> GetAuthors()
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Authors.Include("Person")
                                 .OrderBy(a => a.Person.Lastname)
                                 .ThenBy(a => a.Person.Firstname)
                                 .ToList();
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
            if(number < 0)
            {
                MessageBox.Show("Please enter a positive integer.");
                return;
            }
            using (var db = new LibraryInfoEntities())
            {
                var bookContext = db.Books.FirstOrDefault(b => b.ISBN == book.ISBN);
                bookContext.NumberOfCopies += number;
                db.SaveChanges();
                MessageBox.Show($"Added {number} copies to '{book.Title}'.");
            }
        }

        public static void RemoveBookCopies(Book book, int number)
        {
            if (number < 0)
            {
                MessageBox.Show("Please enter a positive integer.");
                return;
            }
            if (book.NumberOfCopies < number)
            {
                MessageBox.Show("Number of copies cannot be less than zero.");
                return;
            }                
            if (API.GetNumberAvailableBookCopies(book) < number)
            {
                MessageBox.Show("Cannot remove books that are currently checked out.");
                return;
            }                
            using (var db = new LibraryInfoEntities())
            {
                var bookContext = db.Books.FirstOrDefault(b => b.ISBN == book.ISBN);
                bookContext.NumberOfCopies -= number;
                db.SaveChanges();
                MessageBox.Show($"Removed {number} copies from '{book.Title}'.");
            }
        }

        public static List<Librarian> GetAllLibrarians()
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Librarians.Include("Person")
                                    .OrderBy(a => a.Person.Lastname)
                                    .ThenBy(a => a.Person.Firstname)
                                    .ToList();
            }
        }
        
        public static List<Book> GetBooksByAuthor(Author author)
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Books.Where(b => b.AuthorID == author.ID)
                               .OrderBy(b => b.Title)
                               .ToList();
            }
        }

        public static List<Cardholder> GetAllCardholders()
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.Cardholders.Include("Person")
                                     .OrderBy(a => a.Person.Lastname)
                                     .ThenBy(a => a.Person.Firstname)
                                     .ToList();
            }
        }

        public static List<CheckOutLog> GetCheckOutLogByCardholder(Cardholder cardholder)
        {
            using (var db = new LibraryInfoEntities())
            {
                return db.CheckOutLogs.Include("Book")
                                      .Where(c => c.CardholderID == cardholder.ID)
                                      .OrderBy(b => b.Book.Title)
                                      .ToList();
            }
        }

        public static List<CheckOutLog> GetOverdueCheckoutLogs()
        {
            using (var db = new LibraryInfoEntities())
            {
                var dueDate = DateTime.Now.AddDays(-30);
                return db.CheckOutLogs.Include("Book")
                                      .Include("Cardholder.Person")
                                      .Where(c => c.CheckOutDate <= dueDate)
                                      .OrderBy(c => c.CheckOutDate)
                                      .ThenBy(b => b.Book.Title)
                                      .ToList();
            }
        }
    }
}
