using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLibrary;
using AdmissionLibrary;

namespace WebApp.Controllers
{
    public class MainController : Controller
    {
        private DAO dao;
        private Admission admission;

        public MainController()
        {
            dao = new DAO();
            admission = new Admission(dao);
        }

        //
        // GET: /Main/

        [HttpGet]
        public ActionResult Index()
        {
            admission.calculateAndPublishResults();

            ViewBag.DAO = dao;
            ViewBag.Admission = admission;
            return View();
        }

        //
        // POST: /Main/

        [HttpPost]
        public ActionResult Index(Applicant applicant, string submitAction)
        {
            dispatchAction(applicant, submitAction);

            return Index();
        }

        private void dispatchAction(Applicant applicant, string submitAction)
        {
            switch (submitAction)
            {
                case Constants.InsertApplicant:
                    admission.insertApplicant(applicant);
                    break;
                case Constants.UpdateApplicant:
                    admission.updateApplicant(applicant.Cnp, applicant);
                    break;
                case Constants.DeleteApplicant:
                    admission.deleteApplicant(applicant.Cnp);
                    break;
                case Constants.RepopulateDB:
                    admission.populateDB();
                    break;
            }
        }

    }
}
