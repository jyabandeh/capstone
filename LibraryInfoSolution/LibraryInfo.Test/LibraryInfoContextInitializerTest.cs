using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryInfoDataModelClassLibrary;
using LibraryInfoWpfApp;
using LibraryInfo;
using NUnit.Framework;
using Moq;
using System.Xml.Linq;

namespace LibraryInfo.Test
{
    [TestFixture]
    public class LibraryInfoContextInitializerTest
    {
        [Test]
        public void NumberOfRowsFromXmlShoulBeValid()
        {
            var actualPeople = LibraryInfoContextInitializer.GetPeopleFromXML();
            var expectedPeopleCount = 16;
            Assert.AreEqual(expectedPeopleCount, actualPeople.Count, "GetPeopleFromXML should be getting the correct number of rows.");

            var actualAuthors = LibraryInfoContextInitializer.GetAuthorsFromXML();
            var expectedAuthorsCount = 7;
            Assert.AreEqual(expectedAuthorsCount, actualAuthors.Count, "GetAuthorsFromXML should be getting the correct number of rows.");

            var actualCardholders = LibraryInfoContextInitializer.GetCardholdersFromXML();
            var expectedCardholdersCount = 3;
            Assert.AreEqual(expectedCardholdersCount, actualCardholders.Count, "GetCardholdersFromXML should be getting the correct number of rows.");

            var actualLibrarians = LibraryInfoContextInitializer.GetLibrariansFromXML();
            var expectedLibrariansCount = 6;
            Assert.AreEqual(expectedLibrariansCount, actualLibrarians.Count, "GetLibrariansFromXML should be getting the correct number of rows.");

            var actualCheckOutLogs = LibraryInfoContextInitializer.GetCheckOutLogsFromXML();
            var expectedCheckOutLogsCount = 3;
            Assert.AreEqual(expectedCheckOutLogsCount, actualCheckOutLogs.Count, "GetCheckOutLogsFromXML should be getting the correct number of rows.");

            var actualBooks = LibraryInfoContextInitializer.GetBooksFromXML();
            var expectedBooksCount = 7;
            Assert.AreEqual(expectedBooksCount, actualBooks.Count, "GetBooksFromXML should be getting the correct number of rows.");
        }
    }
}
