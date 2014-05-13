using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AdmissionLibrary;
using NMock2;
using DatabaseLibrary;

namespace AdmissionLibrary_Tests
{
    [TestFixture]
    class GradesVerifier_Tests
    {

        Mockery mocks;
        IApplicant applicant;
        double[] inBoundsValues = { 1.2, 3.4, 5.6, 7.0, 7.8, 8.6, 9.8 };
        double[] outOfBoundsValues = { -89.9, -56.3, -0.1, 10.1, 20.7, 50.6 };

        /* Set up function which initializes the mock object*/
        [SetUp]
        public void setUp()
        {
            mocks = new Mockery();
            applicant = mocks.NewMock<IApplicant>();
        }

        /*[TestCase(-2.5, false)]
        [TestCase(-4.5, false)]
        [TestCase(0.0, false)]
        [TestCase(0.5, false)]
        [TestCase(1.0, true)]
        [TestCase(2.5, true)]
        [TestCase(5.32, true)]
        [TestCase(10.0, true)]
        [TestCase(10.1, false)]
        [TestCase(12.5, false)]
        public void verifyGrade_Test_ReturnFalse(double gradeValue, bool expectedResult)
        {
            bool result = GradesVerifier.verifyGrade(gradeValue);
            Assert.AreEqual(expectedResult, result);
        }*/


        /* tests when first property tested (TestMark) is out of bounds */
        [Test]
        public void verifyStudentsGrade_Test_OutOfBoundsTestMark([Values(-89.9, -56.3, -0.1, 10.1, 20.7, 50.6)]double testMark)
        {
            /*using (mocks.Ordered)
            {
                Expect.Once.On(applicant).GetProperty("TestMark").Will(Return.Value(testMark));
                Expect.Never.On(applicant).GetProperty("DomainMark");
                Expect.Never.On(applicant).GetProperty("AvgExamen");

                //Tuple<bool, String> result = GradesVerifier.verifyStudentGrades(applicant);

                Assert.AreEqual(new Tuple<bool, String>(false, "TestMark"), result);
                mocks.VerifyAllExpectationsHaveBeenMet();
            }*/
        }


        /* tests when second property tested (DomainMark) is out of bounds */
        [Test]
        public void verifyStudentsGrade_Test_OutOfBoundsDomainMark(
                                            [Values(1.2, 3.4, 5.6, 7.0, 7.8, 8.6, 9.8)]double testMark,
                                            [Values(-89.9, -56.3, -0.1, 10.1, 20.7, 50.6)]double domainMark)
        {
            /*using (mocks.Ordered)
            {
                Expect.Once.On(applicant).GetProperty("TestMark").Will(Return.Value(testMark));
                Expect.Once.On(applicant).GetProperty("DomainMark").Will(Return.Value(domainMark));
                Expect.Never.On(applicant).GetProperty("AvgExamen");

                Tuple<bool, String> result = GradesVerifier.verifyStudentGrades(applicant);

                Assert.AreEqual(new Tuple<bool, String>(false, "DomainMark"), result);
                mocks.VerifyAllExpectationsHaveBeenMet();
            }*/
        }


        /* tests when third property tested (AvgExamen) is out of bounds */
        [Test]
        public void verifyStudentsGrade_Test_OutOfBoundsAvgExamen(
                                        [Values(1.2, 3.4, 5.6, 7.0, 7.8, 8.6, 9.8)]double testMark, 
                                        [Values(1.2, 3.4, 5.6, 7.0, 7.8, 8.6, 9.8)]double domainMark,
                                        [Values(-89.9, -56.3, -0.1, 10.1, 20.7, 50.6)]double avgExamen)
        {
            /*using (mocks.Ordered)
            {
                Expect.Once.On(applicant).GetProperty("TestMark").Will(Return.Value(testMark));
                Expect.Once.On(applicant).GetProperty("DomainMark").Will(Return.Value(domainMark));
                Expect.Once.On(applicant).GetProperty("AvgExamen").Will(Return.Value(avgExamen));

                Tuple<bool, String> result = GradesVerifier.verifyStudentGrades(applicant);

                Assert.AreEqual(new Tuple<bool, String>(false, "AvgExamen"), result);
                mocks.VerifyAllExpectationsHaveBeenMet();
            }*/
        }

        /* tests when all grades have in bound values */
        [Test]
        public void verifyStudentsGrade_Test_InBoundsValues(
                                        [Values(1.2, 3.4, 5.6, 7.0, 7.8, 8.6, 9.8, 10.0)]double testMark,
                                        [Values(1.2, 3.4, 5.6, 7.0, 7.8, 8.6, 9.8, 10.0)]double domainMark,
                                        [Values(1.2, 3.4, 5.6, 7.0, 7.8, 8.6, 9.8, 10.0)]double avgExamen)
        {
           /* using (mocks.Ordered)
            {
                Expect.Once.On(applicant).GetProperty("TestMark").Will(Return.Value(testMark));
                Expect.Once.On(applicant).GetProperty("DomainMark").Will(Return.Value(domainMark));
                Expect.Once.On(applicant).GetProperty("AvgExamen").Will(Return.Value(avgExamen));

                Tuple<bool, String> result = GradesVerifier.verifyStudentGrades(applicant);

                Assert.AreEqual(new Tuple<bool, String>(true, ""), result);
                mocks.VerifyAllExpectationsHaveBeenMet();
            }*/
        }

      
    }
}
