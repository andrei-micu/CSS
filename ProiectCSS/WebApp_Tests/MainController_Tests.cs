using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NMock2;

using DatabaseLibrary;
using AdmissionLibrary;
using WebApp.Controllers;
using WebApp;

namespace WebApp_Tests
{
    [TestFixture]
    public class MainController_Tests
    {
        private MainController mainController;

        private Mockery mocks;
        private IDAO dao;
        private IAdmission admission;

        private List<IApplicant> applicants;
        private List<IResult> results;

        [SetUp]
        public void setUp()
        {
            mocks = new Mockery();
            dao = mocks.NewMock<IDAO>();
            admission = mocks.NewMock<IAdmission>();

            mainController = new MainController(dao, admission);

            applicants = new List<IApplicant>();
            applicants.Add(new Applicant("2900680155208", "Firstname", "Lastname", "I.", "City", "Locality", "School", 8.70, 9.30, 10.0));

            results = new List<IResult>();
            results.Add(new Result(applicants[0], "some-result"));
        }

        public void expectViewMethods()
        {
            Expect.Once.On(admission).Method("calculateAndPublishResults").WithNoArguments();
            Expect.Once.On(dao).Method("getApplicants").WithNoArguments().Will(Return.Value(applicants));
            Expect.Once.On(dao).Method("getResults").WithNoArguments().Will(Return.Value(results));
        }

        [Test]
        public void test_Index_GET()
        {
            using (mocks.Ordered)
            {
                expectViewMethods();

                mainController.Index();

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void test_Index_POST_Insert()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(admission).Method("insertApplicant").With(applicants[0]);

                expectViewMethods();

                mainController.Index(applicants[0], Constants.InsertApplicant);

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void test_Index_POST_Update()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(admission).Method("updateApplicant").With(applicants[0].Cnp, applicants[0]);

                expectViewMethods();

                mainController.Index(applicants[0], Constants.UpdateApplicant);

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void test_Index_POST_Delete()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(admission).Method("deleteApplicant").With(applicants[0].Cnp);

                expectViewMethods();

                mainController.Index(applicants[0], Constants.DeleteApplicant);

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void test_Index_POST_RepopulateDB()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(admission).Method("populateDB").WithNoArguments();

                expectViewMethods();

                mainController.Index(applicants[0], Constants.RepopulateDB);

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }
    }
}
