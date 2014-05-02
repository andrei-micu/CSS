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
                case "Insert applicant":
                    admission.insertApplicant(applicant);
                    break;
                case "Update applicant":
                    admission.updateApplicant(applicant.Cnp, applicant);
                    break;
                case "Delete applicant":
                    admission.deleteApplicant(applicant.Cnp);
                    break;
                case "Repopulate DB":
                    admission.populateDB();
                    break;
            }
        }

    }
}
