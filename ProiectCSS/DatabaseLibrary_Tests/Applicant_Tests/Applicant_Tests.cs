using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DatabaseLibrary;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLibrary_Tests.Applicant_Tests
{
    [TestFixture]
    class Applicant_Tests
    {
        IApplicant applicant;

        private string[] invalidCnp = { "", "123", "asdf", "asdfghjklerty", "=true" };
        private string[] validCnp = { "1234567890123", "5674893029485"};

        private string[] invalidNames = { "a453", "T34N", "An", "Iu.", "P0lar"};
        private string[] validNames = { "Ana", "Maria", "Tanase", "Copoi", "Grigore" };

        private string[] invalidInitial = { "a", "a.", "H", "3.", "+." };
        private string[] validInitial = { "A.", "B.", "H.", "J.", "I." };

        private string[] invalidLocations = { "alba", "0k", "Al.ba" };
        private string[] validLocations = { "Iasi", "Suceava", "Alba" };

        private double[] inBoundsValues = { 1.2, 3.4, 5.6, 7.0, 7.8, 8.6, 9.8 };
        private double[] outOfBoundsValues = { -89.9, -56.3, -0.1, 10.1, 20.7, 50.6 };

        [SetUp]
        public void setUp()
        {
            applicant = new Applicant();
        }

        [TestCaseSource("invalidCnp")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidCnp(string cnp)
        {
            applicant.Cnp = cnp;
        }

        [TestCaseSource("validCnp")]
        public void test_SetValidCnp(string cnp)
        {
            applicant.Cnp = cnp;
            Assert.AreEqual(cnp, applicant.Cnp);
        }

        [TestCaseSource("invalidNames")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidFirstName(string invalidName)
        {
            applicant.FirstName = invalidName;
        }

        [TestCaseSource("validNames")]
        public void test_SetValidFirstName(string validName)
        {
            applicant.FirstName = validName;
            Assert.AreEqual(validName, applicant.FirstName);
        }

        [TestCaseSource("invalidNames")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidLastName(string invalidName)
        {
            applicant.LastName = invalidName;
        }

        [TestCaseSource("validNames")]
        public void test_SetValidLasttName(string validName)
        {
            applicant.LastName = validName;
            Assert.AreEqual(validName, applicant.LastName);
        }

        [TestCaseSource("invalidInitial")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidFatherInitial(string invalidInitial)
        {
            applicant.FatherInitial = invalidInitial;
        }

        [TestCaseSource("validInitial")]
        public void test_SetValidFatherInitial(string validInitial)
        {
            applicant.FatherInitial = validInitial;
            Assert.AreEqual(validInitial, applicant.FatherInitial);
        }

        [TestCaseSource("invalidLocations")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidCity(string invalidLocation)
        {
            applicant.City = invalidLocation;
        }

        [TestCaseSource("validLocations")]
        public void test_SetValidCity(string validLocation)
        {
            applicant.City = validLocation;
            Assert.AreEqual(validLocation, applicant.City);
        }

        [TestCaseSource("invalidLocations")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidLocality(string invalidLocation)
        {
            applicant.Locality = invalidLocation;
        }
        [TestCaseSource("validLocations")]
        public void test_SetValidLocality(string validLocation)
        {
            applicant.Locality = validLocation;
            Assert.AreEqual(validLocation, applicant.Locality);
        }


        [TestCaseSource("outOfBoundsValues")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidTestMark(double value)
        {
            applicant.TestMark = value;
        }

        [TestCaseSource("inBoundsValues")]
        public void test_SetValidTestMark(double value)
        {
            applicant.TestMark = value;
            Assert.AreEqual(value, applicant.TestMark);
        }

        [TestCaseSource("outOfBoundsValues")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidDomainMark(double value)
        {
            applicant.DomainMark = value;
        }

        [TestCaseSource("inBoundsValues")]
        public void test_SetValidDomainMark(double value)
        {
            applicant.DomainMark = value;
            Assert.AreEqual(value, applicant.DomainMark);
        }

        [TestCaseSource("outOfBoundsValues")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidAvgExamen(double value)
        {
            applicant.AvgExamen = value;
        }

        [TestCaseSource("inBoundsValues")]
        public void test_SetValidAvgExamen(double value)
        {
            applicant.AvgExamen = value;
            Assert.AreEqual(value, applicant.AvgExamen);
        }

        [TestCaseSource("outOfBoundsValues")]
        [ExpectedException(typeof(ValidationException))]
        public void test_SetInvalidGeneralAverage(double value)
        {
            applicant.GeneralAverage = value;
        }

        [TestCaseSource("inBoundsValues")]
        public void test_SetValidGeneralAverage(double value)
        {
            applicant.GeneralAverage = value;
            Assert.AreEqual(value, applicant.GeneralAverage);
        }
        
    }
}
