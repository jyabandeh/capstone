namespace LibraryInfoDataModelClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Librarian
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public virtual Person Person { get; set; }
    }
}
