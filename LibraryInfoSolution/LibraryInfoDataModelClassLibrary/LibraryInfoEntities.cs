namespace LibraryInfoDataModelClassLibrary
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LibraryInfoEntities : DbContext
    {
        public LibraryInfoEntities()
            : base("name=LibraryInfoEntities")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Cardholder> Cardholders { get; set; }
        public virtual DbSet<CheckOutLog> CheckOutLogs { get; set; }
        public virtual DbSet<Librarian> Librarians { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.ISBN)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.YearPublished)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.CheckOutLogs)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cardholder>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Cardholder>()
                .Property(e => e.LibraryCardID)
                .IsUnicode(false);

            modelBuilder.Entity<Cardholder>()
                .HasMany(e => e.CheckOutLogs)
                .WithRequired(e => e.Cardholder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Librarian>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Librarian>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<Librarian>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.Author)
                .WithRequired(e => e.Person);

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.Cardholder)
                .WithRequired(e => e.Person);

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.Librarian)
                .WithRequired(e => e.Person);
        }
    }
}
