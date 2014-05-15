using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NMock2;
using DatabaseLibrary;
using System.IO;

namespace DatabaseLibrary_Tests
{
    public class Database_Tests
    {
        private DAO dao;
        private string initialContent;
        private string APPLICANTS_FILE = "applicants.txt";

        [TestFixtureSetUp]
        public void setUp()
        {
            dao = new DAO();
            dao.checkCreateFile(APPLICANTS_FILE);

            dao.insertApplicant(new Applicant("2900610155203", "Irina", "Naum", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));
            dao.insertApplicant(new Applicant("2891245633221", "Dana", "Harbon", "A.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 7.90, 8.30, 9.50));
            dao.insertApplicant(new Applicant("2904533456878", "Corina", "Micla", "T.", "Iasi", "Iasi", "Colegiul National", 9.20, 9.40, 10.0));
            dao.insertApplicant(new Applicant("1891212566544", "Andrei", "Juve", "V.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.70, 7.80, 10.0));
            dao.insertApplicant(new Applicant("1901009778987", "Serban", "Donise", "B.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 6.45, 9.00, 9.50));
            dao.insertApplicant(new Applicant("1900808776443", "Tudor", "Rent", "R.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.40, 6.30, 10.0));
            dao.insertApplicant(new Applicant("2891023990887", "Renata", "Paun", "A.", "Iasi", "Iasi", "Colegiul National Costache Negruzzi", 9.90, 6.30, 9.50));
            dao.insertApplicant(new Applicant("1890306789086", "George", "Cojun", "T.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.45, 8.90, 9.30));

            initialContent = getContentFromFile(APPLICANTS_FILE);

        }

        [Test]
        public void test_1_GetApplicants()
        {
            List<Applicant> expectedApplicants = new List<Applicant>();
            expectedApplicants.Add(new Applicant("2900610155203", "Irina", "Naum", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));
            expectedApplicants.Add(new Applicant("2891245633221", "Dana", "Harbon", "A.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 7.90, 8.30, 9.50));
            expectedApplicants.Add(new Applicant("2904533456878", "Corina", "Micla", "T.", "Iasi", "Iasi", "Colegiul National", 9.20, 9.40, 10.0));
            expectedApplicants.Add(new Applicant("1891212566544", "Andrei", "Juve", "V.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.70, 7.80, 10.0));
            expectedApplicants.Add(new Applicant("1901009778987", "Serban", "Donise", "B.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 6.45, 9.00, 9.50));
            expectedApplicants.Add(new Applicant("1900808776443", "Tudor", "Rent", "R.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.40, 6.30, 10.0));
            expectedApplicants.Add(new Applicant("2891023990887", "Renata", "Paun", "A.", "Iasi", "Iasi", "Colegiul National Costache Negruzzi", 9.90, 6.30, 9.50));
            expectedApplicants.Add(new Applicant("1890306789086", "George", "Cojun", "T.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.45, 8.90, 9.30));

            CollectionAssert.AreEqual(dao.getApplicants(), expectedApplicants);
        }

        [Test]
        public void test_2_Insert_ExistentApplicant()
        {
            dao.insertApplicant(new Applicant("1890306789086", "Alexandra", "Miron", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));
            StringAssert.AreEqualIgnoringCase(getContentFromFile(APPLICANTS_FILE), initialContent);
        }

        [Test]
        public void test_3_Update_InexistentApplicant()
        {
            dao.updateApplicant("9990306789086", new Applicant("9990101987488", "Marius", "Miron", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));
            StringAssert.AreEqualIgnoringCase(getContentFromFile(APPLICANTS_FILE), initialContent);
        }

        [Test]
        public void test_4_Delete_InexistentApplicant()
        {
            dao.deleteApplicant("9990306789086");
            StringAssert.AreEqualIgnoringCase(getContentFromFile(APPLICANTS_FILE), initialContent);
        }

        [Test]
        public void test_InsertApplicant()
        {
            dao.insertApplicant(new Applicant("9999999999000", "Alexandra", "Miron", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));
            string content = getContentFromFile(APPLICANTS_FILE);
            StringAssert.Contains("9999999999000", content);
        }


        [Test]
        public void test_UpdateApplicant()
        {
            dao.updateApplicant("9999999999000", new Applicant("1890101987488", "Marius", "Miron", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));
            string content = getContentFromFile(APPLICANTS_FILE);
            StringAssert.Contains("1890101987488,Marius,Miron,V.,Iasi,Iasi,Colegiul National Emil Racovita,8.7,9.3,10,9.21", content);
        }


        [Test]
        public void teste_DeleteApplicant()
        {
            dao.deleteApplicant("9994533456878");
            string content = getContentFromFile(APPLICANTS_FILE);
            StringAssert.DoesNotContain("9994533456878,Corina,Micla,T.,Iasi,Iasi,Colegiul National, 9.20, 9.40, 10.0", content);
        }


        [TestFixtureTearDown]
        public void restoreState()
        {
            System.IO.File.WriteAllText(APPLICANTS_FILE, initialContent);
        }

        public string getContentFromFile(string filename)
        {
            return System.IO.File.ReadAllText(APPLICANTS_FILE);
        }

    }
}
