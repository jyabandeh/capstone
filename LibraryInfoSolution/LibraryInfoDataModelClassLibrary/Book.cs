namespace LibraryInfoDataModelClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            CheckOutLogs = new HashSet<CheckOutLog>();
        }

        public int BookID { get; set; }

        [Required]
        [StringLength(50)]
        public string ISBN { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public int AuthorID { get; set; }

        public int? NumberPages { get; set; }

        [StringLength(32)]
        public string Subject { get; set; }

        [StringLength(1028)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Publisher { get; set; }

        [StringLength(4)]
        public string YearPublished { get; set; }

        [StringLength(32)]
        public string Language { get; set; }

        public int NumberOfCopies { get; set; }

        public virtual Author Author { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckOutLog> CheckOutLogs { get; set; }

        public override string ToString()
        {
            return $"Book ID: {BookID}\n{ISBN}\n{Title}";
        }
    }
}
