using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DatabaseLibrary;
using NMock2;
using AdmissionLibrary;
using System.IO;

namespace AdmissionLibrary_Tests
{
    // customize Matcher to see if two lists are equal
    internal class ListMatcher : Matcher
    {
        private IList<IResult> list;

        public ListMatcher(IList<IResult> results)
        {
            this.list = results;
        }

        public override bool Matches(object o)
        {            
            if (!(o is IList<IResult>)) return false;
            IList<IResult> otherList = (IList<IResult>)o;

            if (list.Count != otherList.Count) return false;
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(otherList[i]))
                    return false;               
            }
            return true;
        }

        public override void DescribeTo(TextWriter writer)
        {
            writer.Write("List:");
            foreach (object o in list)
            {
                writer.Write(o.ToString() + " ");
            }
        }
    }

    public class IsList
    {
        public static Matcher Equal(IList<IResult> otherList)
        {
            return new ListMatcher(otherList);
        }
    }


    [TestFixture]
    public class Admission_Tests
    {
       
        private IApplicant applicant;
        private IList<IApplicant> givenApplicants;
        private IList<IResult> expectedResults;
        private IList<IApplicant> applicants;
        private IList<IResult> results;
        private IResult result;
        private IDAO dao;
        private Mockery mocks;
        private Admission admission;

        [SetUp]
        public void setUp()
        {
            mocks = new Mockery();
            
            applicant = mocks.NewMock<IApplicant>();
            applicants = mocks.NewMock<IList<IApplicant>>();
            
            result = mocks.NewMock<IResult>();
            results = mocks.NewMock<IList<IResult>>();

            dao = mocks.NewMock<IDAO>();
            
            admission = new Admission(dao);
        }


        [Test]
        public void test_NullApplicant_Insertion()
        {
            Expect.Never.On(dao).Method("insertApplicant");
            admission.insertApplicant(null);

            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void test_NullApplicant_Update([Values("", "123456789", "1234567890123")]string cnp)
        {
            Expect.Never.On(dao).Method("insertApplicant");
            admission.updateApplicant("", null);

            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [TestCase(10.0, 10.0, 10.0, 10.0)]
        [TestCase(5.6, 7.82, 8.70, 6.886)]
        public void test_InsertApplicant_GeneralAverageSuccess(double testMark, double domainMark, double avgExam, double generalAverage)
        {
            using (mocks.Ordered)
            {
                /* the average is calculated */
                Expect.Once.On(applicant).GetProperty("TestMark").Will(Return.Value(testMark));
                Expect.Once.On(applicant).GetProperty("DomainMark").Will(Return.Value(domainMark));
                Expect.Once.On(applicant).GetProperty("AvgExamen").Will(Return.Value(avgExam));

                Expect.Once.On(applicant).SetProperty("GeneralAverage").To(generalAverage);
                Expect.Once.On(dao).Method("insertApplicant").With(applicant);

                admission.insertApplicant(applicant);

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }


        [TestCase(10.0, 10.0, 10.0, 10.0, "1234567890123")]
        [TestCase(5.6, 7.82, 8.70, 6.886, "1234567890123")]
        public void test_UpdateApplicant_GeneralAverageSuccess(double testMark, double domainMark, double avgExam,
                                                            double generalAverage, string cnp)
        {
            using (mocks.Ordered)
            {
                /* the average is calculated */
                Expect.Once.On(applicant).GetProperty("TestMark").Will(Return.Value(testMark));
                Expect.Once.On(applicant).GetProperty("DomainMark").Will(Return.Value(domainMark));
                Expect.Once.On(applicant).GetProperty("AvgExamen").Will(Return.Value(avgExam));

                Expect.Once.On(applicant).SetProperty("GeneralAverage").To(generalAverage);
                Expect.Once.On(dao).Method("updateApplicant").With(cnp, applicant);

                admission.updateApplicant(cnp, applicant);

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void test_calculateAndPublishResults_OneTypeOfResult()
        {
            //Mocking the list with a customizable matcher does not work
            /*Expect.Once.On(applicants).Method("Add");
            Expect.AtLeastOnce.On(applicant).SetProperty("Cnp");
            Expect.AtLeastOnce.On(applicant).SetProperty("FirstName");
            Expect.AtLeastOnce.On(applicant).SetProperty("LastName");
            Expect.AtLeastOnce.On(applicant).SetProperty("FatherInitial");
            Expect.AtLeastOnce.On(applicant).SetProperty("City");
            Expect.AtLeastOnce.On(applicant).SetProperty("Locality");
            Expect.AtLeastOnce.On(applicant).SetProperty("SchoolName");
            Expect.AtLeastOnce.On(applicant).SetProperty("TestMark");
            Expect.AtLeastOnce.On(applicant).SetProperty("DomainMark");
            Expect.AtLeastOnce.On(applicant).SetProperty("AvgExamen");
            Expect.AtLeastOnce.On(applicant).SetProperty("GeneralAverage");

            applicant.Cnp = "2900680155208";
            applicant.FirstName = "Alexandra";
            applicant.LastName = "Miron";
            applicant.FatherInitial = "V.";
            applicant.City = "Iasi";
            applicant.Locality = "Iasi";
            applicant.SchoolName = "Colegiul National Emil Racovita";
            applicant.TestMark =  10.0;
            applicant.DomainMark = 10.0;
            applicant.AvgExamen = 10.0;
            applicant.GeneralAverage = 10.0;
            applicants.Add(applicant);
            */
            /*
               Expect.Once.On(results).Method("Add");

               Expect.Once.On(result).SetProperty("applicant").To(applicant);
               Expect.Once.On(result).SetProperty("result").To("ADMIS/BUGET");
               result.applicant = applicant;
               result.result = "ADMIS/BUGET";
               results.Add(new Result(new Applicant("2900680155208", "Alexandra", "Miron", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 10.0, 10.0, 10.0), "ADMIS/BUGET"));
            */
            /*
             * //Expect.AtLeastOnce.On(applicants).GetProperty("Count").Will(Return.Value(1));
            //Expect.AtLeastOnce.On(applicants).Method("CopyTo");
            //Expect.AtLeastOnce.On(applicants).Method("get_Item").With(0);
             * /
            /* Expect.AtLeastOnce.On(results).Method("CopyTo");
           Expect.AtLeastOnce.On(results).GetProperty("Count").Will(Return.Value(1));
           Expect.AtLeastOnce.On(results).Method("get_Item").With(0).Will(Return.Value(result));*/

            using (mocks.Ordered)
            {
                /* create the list the dao mock object should return when calling getApplicants */
                givenApplicants = new List<IApplicant>();
                IApplicant applicant = new Applicant("2904533456878", "Corina", "Micla", "T.", "Iasi", "Iasi", "Colegiul National", 9.20, 9.40, 10.0);
                givenApplicants.Add(applicant);

                /* mock the behaviour of getApplicants method providing the param*/
                Expect.Once.On(dao).Method("getApplicants").Will(Return.Value(givenApplicants));

                Expect.Once.On(dao).Method("clearResults");

                /* construct the list of the expected results */
                Result result = new Result(new Applicant("2904533456878", "Corina", "Micla", "T.", "Iasi", "Iasi", "Colegiul National", 9.20, 9.40, 10.0), "ADMIS/BUGET");
                expectedResults = new List<IResult> { result };

                /* mock the behaviour of the insertResults method testing that the passed argument is the expected results list */
                Expect.Once.On(dao).Method("insertResults").With(IsList.Equal(expectedResults));//.With(IsList.Equal(results.Cast<IResult>().ToList()));
                admission.calculateAndPublishResults();
                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }


        [Test]
        public void test_CalculateAndPublishResults_TwoTypesOfResults()
        {
            using (mocks.Ordered)
            {
                givenApplicants = new List<IApplicant>();
                givenApplicants.Add(new Applicant("2891023990887", "Renata", "Paun", "A.", "Iasi", "Iasi", "Colegiul National Costache Negruzzi", 9.90, 6.30, 9.50));
                givenApplicants.Add(new Applicant("1890306789086", "George", "Cojun", "T.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.45, 8.90, 9.30));

                expectedResults = new List<IResult>();
                expectedResults.Add(new Result(new Applicant("2891023990887", "Renata", "Paun", "A.", "Iasi", "Iasi", "Colegiul National Costache Negruzzi", 9.90, 6.30, 9.50), "ADMIS/BUGET"));
                expectedResults.Add(new Result(new Applicant("1890306789086", "George", "Cojun", "T.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.45, 8.90, 9.30), "ADMIS/TAXA"));

                /* mock the behaviour of getApplicants method providing the param*/
                Expect.Once.On(dao).Method("getApplicants").Will(Return.Value(givenApplicants));

                Expect.Once.On(dao).Method("clearResults");

                /* mock the behaviour of the insertResults method testing that the passed argument is the expected results list */              
                Expect.Once.On(dao).Method("insertResults").With(IsList.Equal(expectedResults));
                admission.calculateAndPublishResults();
                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void test_CalculateAndPublishResults_AllTypesOfResults()
        {
            using (mocks.Ordered)
            {
                /* populate the list which will be passed to the dao mock method of getApplicants */
                givenApplicants = new List<IApplicant>();
                givenApplicants.Add(new Applicant("2900610155203", "Irina", "Naum", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));
                givenApplicants.Add(new Applicant("2891245633221", "Dana", "Harbon", "A.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 7.90, 8.30, 9.50));
                givenApplicants.Add(new Applicant("2904533456878", "Corina", "Micla", "T.", "Iasi", "Iasi", "Colegiul National", 9.20, 9.40, 10.0));
                givenApplicants.Add(new Applicant("1891212566544", "Andrei", "Juve", "V.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.70, 7.80, 10.0));
                givenApplicants.Add(new Applicant("1901009778987", "Serban", "Donise", "B.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 6.45, 9.00, 9.50));
                givenApplicants.Add(new Applicant("1900808776443", "Tudor", "Rent", "R.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.40, 6.30, 10.0));
                givenApplicants.Add(new Applicant("2891023990887", "Renata", "Paun", "A.", "Iasi", "Iasi", "Colegiul National Costache Negruzzi", 9.90, 6.30, 9.50));
                givenApplicants.Add(new Applicant("1890306789086", "George", "Cojun", "T.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.45, 8.90, 9.30));

                /* setup the set of expected results */
                expectedResults = new List<IResult>();
                expectedResults.Add(new Result(new Applicant("2904533456878", "Corina", "Micla", "T.", "Iasi", "Iasi", "Colegiul National", 9.20, 9.40, 10.0), "ADMIS/BUGET"));
                expectedResults.Add(new Result(new Applicant("1891212566544", "Andrei", "Juve", "V.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.70, 7.80, 10.0), "ADMIS/TAXA"));
                expectedResults.Add(new Result(new Applicant("2900610155203", "Irina", "Naum", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0), "ADMIS/TAXA"));
                expectedResults.Add(new Result(new Applicant("2891023990887", "Renata", "Paun", "A.", "Iasi", "Iasi", "Colegiul National Costache Negruzzi", 9.90, 6.30, 9.50), "ADMIS/TAXA"));
                expectedResults.Add(new Result(new Applicant("1900808776443", "Tudor", "Rent", "R.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.40, 6.30, 10.0), "ADMIS/TAXA"));
                expectedResults.Add(new Result(new Applicant("1890306789086", "George", "Cojun", "T.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.45, 8.90, 9.30), "ADMIS/TAXA"));
                expectedResults.Add(new Result(new Applicant("2891245633221", "Dana", "Harbon", "A.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 7.90, 8.30, 9.50), "RESPINS"));
                expectedResults.Add(new Result(new Applicant("1901009778987", "Serban", "Donise", "B.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 6.45, 9.00, 9.50), "RESPINS"));                
                
                /* mock the behaviour of getApplicants method providing the param*/
                Expect.Once.On(dao).Method("getApplicants").Will(Return.Value(givenApplicants));

                Expect.Once.On(dao).Method("clearResults");

                /* mock the behaviour of the insertResults method testing that the passed argument is the expected results list */
                Expect.Once.On(dao).Method("insertResults").With(IsList.Equal(expectedResults));
                admission.calculateAndPublishResults();
                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void test_calculateAndPublishResults_EmptyList()
        {
            using (mocks.Ordered)
            {
                /* mock the behaviour of getApplicants method providing as param an empty list*/
                Expect.Once.On(dao).Method("getApplicants").Will(Return.Value(applicants));
                Expect.Once.On(applicants).GetProperty("Count").Will(Return.Value(0));
                Expect.Never.On(applicants).Method("CopyTo");
                Expect.Never.On(dao).Method("clearResults");
                Expect.Never.On(dao).Method("insertResults");
                admission.calculateAndPublishResults();
                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void test_DeleteApplicant([Values("","1234567890123")] string cnp)
        {
            using (mocks.Ordered)
            {
                /* mock the behaviour of deleteApplicant method */
                Expect.Once.On(dao).Method("deleteApplicant").With(cnp);
                
                admission.deleteApplicant(cnp);
                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }


        [Test]
        public void test_populateDatabase()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(dao).Method("clearApplicants");
                Expect.AtLeastOnce.On(dao).Method("insertApplicant");

                admission.populateDB();
            }
        }

    

        
    }
}
