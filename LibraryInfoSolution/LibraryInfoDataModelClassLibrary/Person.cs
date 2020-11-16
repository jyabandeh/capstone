namespace LibraryInfoDataModelClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Person
    {
        public int PersonID { get; set; }

        [Required]
        [StringLength(30)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }

        public virtual Author Author { get; set; }

        public virtual Cardholder Cardholder { get; set; }

        public virtual Librarian Librarian { get; set; }
    }
}
