namespace LibraryInfoDataModelClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckOutLog")]
    public partial class CheckOutLog
    {
        public int CheckOutLogID { get; set; }

        public int CardholderID { get; set; }

        public int BookID { get; set; }

        public DateTime CheckOutDate { get; set; }

        public virtual Book Book { get; set; }

        public virtual Cardholder Cardholder { get; set; }

        public override string ToString()
        {
            return CheckOutDate.ToString();
        }
    }
}
