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
        private IDAO dao;
        private IAdmission admission;

        public MainController()
        {
            this.dao = new DAO();
            this.admission = new Admission(dao);
        }

        public MainController(IDAO dao, IAdmission admission)
        {
            this.dao = dao;
            this.admission = admission;
        }

        //
        // GET: /Main/

        [HttpGet]
        public ActionResult Index()
        {
            admission.calculateAndPublishResults();

            ViewBag.Applicants = dao.getApplicants();
            ViewBag.Results = dao.getResults();
            return View();
        }

        //
        // POST: /Main/

        [HttpPost]
        public ActionResult Index(IApplicant applicant, string submitAction)
        {
            dispatchAction(applicant, submitAction);

            return Index();
        }

        private void dispatchAction(IApplicant applicant, string submitAction)
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
