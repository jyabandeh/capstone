using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Xml.Linq;

namespace LibraryInfoDataModelClassLibrary
{
    public class LibraryInfoContextInitializer : DropCreateDatabaseIfModelChanges<LibraryInfoEntities>
    {
        protected override void Seed(LibraryInfoEntities context)
        {
            //load data from xml
            List<Person> people = GetPeopleFromXML();
            foreach(Person p in people)
            {
                context.People.Add(p);
            }
            context.SaveChanges();

            List<Librarian> librarians = GetLibrariansFromXML();
            foreach(Librarian l in librarians)
            {
                //l.Person = context.People.Where(p => p.PersonID == l.ID).First();                
                context.Librarians.Add(l);
            }
            context.SaveChanges();

            List<Author> authors = GetAuthorsFromXML();
            foreach(Author a in authors)
            {
                //a.Person = context.People.Where(p => p.PersonID == a.ID).First();
                context.Authors.Add(a);
            }
            context.SaveChanges();

            List<Cardholder> cardholders = GetCardholdersFromXML();
            foreach(Cardholder c in cardholders)
            {
                c.Person = context.People.Where(p => p.PersonID == c.ID).First();
                context.Cardholders.Add(c);
            }
            context.SaveChanges();

            List<Book> books = GetBooksFromXML();
            foreach(Book b in books)
            {
                //b.Author
                context.Books.Add(b);
            }
            context.SaveChanges();

            List<CheckOutLog> checkOutLogs = GetCheckOutLogsFromXML();
            foreach(CheckOutLog c in checkOutLogs)
            {
                context.CheckOutLogs.Add(c);
            }
            context.SaveChanges();
        }

        public static List<Author> GetAuthorsFromXML()
        {
            //get contents of xml file
            var authorXML = (from d in XDocument.Load(@"XMLFiles\Authors.xml").Descendants("Author")
                             select d).ToList();

            //set up collection to store data from xml file
            List<Author> authors = new List<Author>(authorXML.Count);

            foreach(var a in authorXML)
            {
                Author author = new Author();
                author.ID = int.Parse(a.Element("ID").Value);
                author.Bio = a.Element("Bio")?.Value.Trim();

                authors.Add(author);
            }

            return authors;
        }

        public static List<Book> GetBooksFromXML()
        {
            var bookXML = (from d in XDocument.Load(@"XMLFiles\Books.xml").Descendants("Book")
                             select d).ToList();

            List<Book> books = new List<Book>(bookXML.Count);

            foreach(var b in bookXML)
            {
                Book book = new Book();
                book.BookID = int.Parse(b.Element("BookID").Value);
                book.ISBN = b.Element("ISBN").Value;
                book.Title = b.Element("Title").Value;
                book.AuthorID = int.Parse(b.Element("AuthorID").Value);
                var numPages = b.Element("NumPages")?.Value;
                if (numPages != null)
                    book.NumberPages = int.Parse(numPages);
                book.Subject = b.Element("Subject")?.Value;
                book.Description = b.Element("Description")?.Value;
                book.Publisher = b.Element("Publisher")?.Value;
                book.YearPublished = b.Element("YearPublished")?.Value;
                book.Language = b.Element("Language")?.Value;
                var numCopies = b.Element("NumberOfCopies")?.Value;
                if (numCopies != null)
                    book.NumberOfCopies = int.Parse(numCopies);

                books.Add(book);
            }

            return books;
        }

        public static List<Cardholder> GetCardholdersFromXML()
        {
            var cardholderXML = (from d in XDocument.Load(@"XMLFiles\Cardholders.xml").Descendants("Cardholder")
                                 select d).ToList();

            List<Cardholder> cardholders = new List<Cardholder>(cardholderXML.Count);

            foreach(var c in cardholderXML)
            {
                Cardholder cardholder = new Cardholder();
                cardholder.ID = int.Parse(c.Element("ID").Value);
                cardholder.Phone = c.Element("Phone").Value;
                cardholder.LibraryCardID = c.Element("LibraryCardID").Value;

                cardholders.Add(cardholder);
            }

            return cardholders;
        }

        public static List<CheckOutLog> GetCheckOutLogsFromXML()
        {
            var checkOutLogXML = (from d in XDocument.Load(@"XMLFiles\CheckOutLog.xml").Descendants("CheckOutLog")
                                  select d).ToList();

            List<CheckOutLog> checkOutLogs = new List<CheckOutLog>(checkOutLogXML.Count);

            foreach(var c in checkOutLogXML)
            {
                CheckOutLog checkOutLog = new CheckOutLog();
                checkOutLog.CheckOutLogID = int.Parse(c.Element("CheckOutLogID").Value);
                checkOutLog.CardholderID = int.Parse(c.Element("CardholderID").Value);
                checkOutLog.BookID = int.Parse(c.Element("BookID").Value);
                checkOutLog.CheckOutDate = DateTime.Parse(c.Element("CheckOutDate").Value);

                checkOutLogs.Add(checkOutLog);
            }

            return checkOutLogs;
        }

        public static List<Librarian> GetLibrariansFromXML()
        {
            var librarianXML = (from d in XDocument.Load(@"XMLFiles\Librarians.xml").Descendants("Librarian")
                                select d).ToList();

            List<Librarian> librarians = new List<Librarian>(librarianXML.Count);

            foreach(var l in librarianXML)
            {
                Librarian librarian = new Librarian();
                librarian.ID = int.Parse(l.Element("ID").Value);
                librarian.Phone = l.Element("Phone").Value;
                librarian.UserID = l.Element("UserID").Value;
                librarian.Password = l.Element("Password").Value;
                
                librarians.Add(librarian);
            }

            return librarians;
        }

        public static List<Person> GetPeopleFromXML()
        {
            var personXML = (from d in XDocument.Load(@"XMLFiles\People.xml").Descendants("Person")
                             select d).ToList();

            List<Person> people = new List<Person>(personXML.Count);

            foreach(var p in personXML)
            {
                Person person = new Person();
                person.PersonID = int.Parse(p.Element("PersonID").Value);
                person.Firstname = p.Element("FirstName").Value;
                person.Lastname = p.Element("LastName").Value;

                people.Add(person);
            }

            return people;
        }
    }
}
