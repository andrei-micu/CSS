using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NMock2;

using WindowsApp;
using DatabaseLibrary;
using AdmissionLibrary;

using System.Windows.Forms;

namespace WindowsApp_Tests
{
    [TestFixture]
    class MainWindow_Tests
    {
        private MainWindow mainWindow;

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

            mainWindow = new MainWindow(dao, admission);

            applicants = new List<IApplicant>();
            applicants.Add(new Applicant("1890522846284", "Ion", "Popescu", "V.", "Iasi",
                "Iasi", "Colegiul National", 7.00, 8.50, 6.75));

            results = new List<IResult>();
            results.Add(new Result(applicants[0], "ADMIS"));
        }

        [Test]
        public void AddApplicantEmptyFields()
        {
            mainWindow.cnp_textbox.Text = "";

            using (mocks.Ordered)
            {
                Expect.Never.On(admission).Method("insertApplicant").With(applicants[0]);
                Expect.Never.On(dao).Method("getApplicants").WithNoArguments().Will(Return.Value(applicants));
                Expect.Never.On(admission).Method("calculateAndPublishResults").WithNoArguments();
                Expect.Never.On(dao).Method("getResults").WithNoArguments().Will(Return.Value(results));

                mainWindow.submit_button.PerformClick();

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void AddApplicantCompletedFields()
        {
            mainWindow.cnp_textbox.Text = "1890522846284";
            mainWindow.first_name_textbox.Text = "Ion";
            mainWindow.father_initial_textbox.Text = "V";
            mainWindow.last_name_textbox.Text = "Popescu";
            mainWindow.city_textbox.Text = "Iasi";
            mainWindow.locality_textbox.Text = "Iasi";
            mainWindow.school_name_textbox.Text = "Colegiul National";
            mainWindow.exam_average_textbox.Text = "8.50";
            mainWindow.domain_mark_textbox.Text = "6.75";
            mainWindow.test_mark_textbox.Text = "7.00";

            using (mocks.Ordered)
            {
                Expect.Once.On(admission).Method("insertApplicant").With(applicants[0]);
                Expect.Once.On(dao).Method("getApplicants").WithNoArguments().Will(Return.Value(applicants));
                Expect.Once.On(admission).Method("calculateAndPublishResults").WithNoArguments();
                Expect.Once.On(dao).Method("getResults").WithNoArguments().Will(Return.Value(results));

                mainWindow.add_button_Click(mainWindow, new EventArgs());

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void UpdateDataTest()
        {
            using (mocks.Ordered)
            {
                Expect.Once.On(dao).Method("getApplicants").WithNoArguments().Will(Return.Value(applicants));
                Expect.Once.On(admission).Method("calculateAndPublishResults").WithNoArguments();
                Expect.Once.On(dao).Method("getResults").WithNoArguments().Will(Return.Value(results));

                mainWindow.UpdateData();

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void UpdateApplicantTest()
        {
            mainWindow.applicants_datagridview.DataSource = applicants;

            using (mocks.Ordered)
            {
                Expect.Once.On(dao).Method("getApplicants").WithNoArguments().Will(Return.Value(applicants));
                Expect.Once.On(admission).Method("updateApplicant").With(applicants[0].Cnp, applicants[0]);
                Expect.Once.On(dao).Method("getApplicants").WithNoArguments().Will(Return.Value(applicants));
                Expect.Once.On(admission).Method("calculateAndPublishResults").WithNoArguments();
                Expect.Once.On(dao).Method("getResults").WithNoArguments().Will(Return.Value(results));

                mainWindow.applicants_datagridview_CellValueChanged(mainWindow, new DataGridViewCellEventArgs(0, 0));

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }

        [Test]
        public void DeleteApplicantTest()
        {
            mainWindow.applicants_datagridview.DataSource = applicants;
            mainWindow.applicants_datagridview.Rows[0].Selected = true;

            using (mocks.Ordered)
            {
                Expect.Once.On(admission).Method("deleteApplicant").With(applicants[0].Cnp);
                Expect.Once.On(dao).Method("getApplicants").WithNoArguments().Will(Return.Value(applicants));
                Expect.Once.On(admission).Method("calculateAndPublishResults").WithNoArguments();
                Expect.Once.On(dao).Method("getResults").WithNoArguments().Will(Return.Value(results));

                mainWindow.applicants_datagridview_KeyDown(mainWindow, new KeyEventArgs(Keys.Delete));

                mocks.VerifyAllExpectationsHaveBeenMet();
            }
        }
    }
}
