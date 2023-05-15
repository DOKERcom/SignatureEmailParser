using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignatureEmailParser.Helpers;
using SignatureEmailParser.Interfaces;
using SignatureEmailParser.Models;

namespace SignatureEmailParser.Tests
{
    [TestClass]
    public class MainUnitTest
    {
        ISignatureParser signatureParser = new SignatureParser();

        [TestMethod]
        public void TestParse_1()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "1.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.City, "Bristol");

            Assert.AreEqual(result.Postcode, "BS16HU");

            Assert.AreEqual(result.Position, "Engineering Project Manager");

            Assert.AreEqual(result.WorkEmail, "s.dunphy@progressivege.com");

            Assert.AreEqual(result.Website, "www.huxleyengineering.com");

            Assert.AreEqual(result.Name, "Simon");

            Assert.AreEqual(result.Surname, "Dunphy");
        }


        [TestMethod]
        public void TestParse_2()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "2.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Mobile, "0118 4024 664");

            Assert.AreEqual(result.Website, "www.avantirec.com");

            //Assert.AreEqual(result.Name, "Alanah"); //no name

            //Assert.AreEqual(result.Surname, "Townley"); //no name

            // Assert.AreEqual(result.linkedin, "Avanti Recruitment Ltd"); //in progress //TODO: LinkedIn: Avanti Recruitment Ltd
        }

        [TestMethod]
        public void TestParse_3()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "3.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.City, "Bristol"); //Problem with email

            Assert.AreEqual(result.Postcode, "BS16HU");

            Assert.AreEqual(result.Mobile, "01179 103333");

            Assert.AreEqual(result.WorkEmail, "d.newman@computerfutures.com");

            Assert.AreEqual(result.Website, "www.computerfutures.com");

            Assert.AreEqual(result.Name, "Daisy");

            Assert.AreEqual(result.Surname, "Newman");
        }

        [TestMethod]
        public void TestParse_4()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "4.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.City, "Bristol");

            Assert.AreEqual(result.Postcode, "BS16HU");

            Assert.AreEqual(result.Mobile, "01179 388088");

            Assert.AreEqual(result.WorkEmail, "s.dunphy@progressivege.com");

            Assert.AreEqual(result.Website, "www.huxleyengineering.com");

            Assert.AreEqual(result.Name, "Simon");

            Assert.AreEqual(result.Surname, "Dunphy");
        }

        [TestMethod]
        public void TestParse_5()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "5.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.City, "Bristol");

            Assert.AreEqual(result.Postcode, "BS16HU");

            Assert.AreEqual(result.Mobile, "01179 103333");

            Assert.AreEqual(result.WorkEmail, "j.anderson@computerfutures.com");

            Assert.AreEqual(result.Website, "www.computerfutures.com");

            Assert.AreEqual(result.Name, "James");

            Assert.AreEqual(result.Surname, "Anderson");
        }

        [TestMethod]
        public void TestParse_6()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "6.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Position,"Senior Consultant");

            Assert.AreEqual(result.Mobile, "+44 (0) 121 647 6888");

            Assert.AreEqual(result.WorkEmail, "alex.pantechis@oscar-tech.com");

            Assert.AreEqual(result.Name, "Alex");

            Assert.AreEqual(result.Surname, "Pantechis");
        }

        [TestMethod]
        public void TestParse_7()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "7.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.City, "Gloucester");

            Assert.AreEqual(result.Region, "Gloucester");

        }

        [TestMethod]
        public void TestParse_8()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "8.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Mobile, "07980610737");

            Assert.AreEqual(result.LinkedIn, "https://www.linkedin.com/in/tim-stock-160177159/");

            Assert.AreEqual(result.WorkEmail, "tstock@pg-rec.com");

        }

        [TestMethod]
        public void TestParse_9()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "9.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.Region, "England");

            Assert.AreEqual(result.City, "Birmingham");

            Assert.AreEqual(result.Mobile, "+44-(0)121-712-1252");

            Assert.AreEqual(result.Website, "www.ChurchofJesusChrist.org");

            Assert.AreEqual(result.Name, "Jonathan");

            Assert.AreEqual(result.Surname, "Berry");
        }

        [TestMethod]
        public void TestParse_10()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "10.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.City, "Newchapel");

            Assert.AreEqual(result.Region, "London");

            Assert.AreEqual(result.Postcode, "RH76HW");

            Assert.AreEqual(result.Mobile, "+44 (0)1342 831403");

            Assert.AreEqual(result.WorkEmail, "elsmith@ChurchOfJesusChrist.org");

            Assert.AreEqual(result.Name, "Elizabeth");

            Assert.AreEqual(result.Surname, "Smith");
        }

        [TestMethod]
        public void TestParse_11()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "11.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.LinkedIn, "linkedin.com/in/emilia-himsworth-25a1851b5");

            Assert.AreEqual(result.Mobile, "01618718811");

            // Assert.AreEqual(result.Name, "Emilia"); //Name only

        }

        [TestMethod]
        public void TestParse_12()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "12.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.City, "Bristol");

            Assert.AreEqual(result.Postcode, "EC4N7BE");

            Assert.AreEqual(result.Mobile, "020 7469 5050");

            Assert.AreEqual(result.Position, "Financial Services");

            Assert.AreEqual(result.WorkEmail, "r.fitzpatrick@huxley.com");

            Assert.AreEqual(result.Website, "www.huxley.com");
        }

        [TestMethod]
        public void TestParse_13()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "13.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Country, "United Kingdom");

            Assert.AreEqual(result.City, "Cardiff");

            Assert.AreEqual(result.Region, "Cardiff");

            Assert.AreEqual(result.Position, "Application Engineer");

            Assert.AreEqual(result.Mobile, "+44 (0) 118 902 8800");

            Assert.AreEqual(result.Website, "www.pg-rec.com");

            Assert.AreEqual(result.Name, "Perry");

            Assert.AreEqual(result.Surname, "Flynn");
        }

        [TestMethod]
        public void TestParse_14()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "14.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }

        [TestMethod]
        public void TestParse_15()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "15.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }

        [TestMethod]
        public void TestParse_16()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "16.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }

        [TestMethod]
        public void TestParse_17()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "17.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }

        [TestMethod]
        public void TestParse_18()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "18.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }


        [TestMethod]
        public void TestParse_19()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "19.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }


        [TestMethod]
        public void TestParse_20()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "20.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }

        [TestMethod]
        public void TestParse_21()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "21.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }

        [TestMethod]
        public void TestParse_22()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "22.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }


        [TestMethod]
        public void TestParse_23()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "23.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

        }


        [TestMethod]
        public void TestParse_24()
        {
            string data = FileHelper.ReadAllFromFile("Emails/", "24.txt");

            EmailParseModel result = signatureParser.Parse(data).Result;

            Assert.AreEqual(result.Position, "Recruitment Consultant");

            Assert.AreEqual(result.Mobile, "+44 (0)7581 141 969");

            //Assert.AreEqual(result.Website, "www.davies-group.com/Consulting"); ??

            Assert.AreEqual(result.Name, "Vikram");

            Assert.AreEqual(result.Surname, "Sidhu");
        }
    }
}