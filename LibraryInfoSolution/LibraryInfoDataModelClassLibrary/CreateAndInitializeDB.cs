using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LibraryInfoDataModelClassLibrary
{
    public class CreateAndInitializeDB
    {
        public static void CreateAndInitializeDatabase()
        {
            Database.SetInitializer<LibraryInfoEntities>(new LibraryInfoContextInitializer());

            // The LINQ below is strictly to cause the database to be created
            //    and initialized, if it does not already exist.  The data
            //    returned is not being used for anything
            using (LibraryInfoEntities db = new LibraryInfoEntities())
            {
                Person person = (from p in db.People
                                 select p).FirstOrDefault();
            }
        }
    }
}
