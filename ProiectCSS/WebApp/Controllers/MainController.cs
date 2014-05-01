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

            ViewBag.DAO = dao;
            ViewBag.Admission = admission;
            return View();
        }

        private void dispatchAction(Applicant applicant, string submitAction)
        {

        }

    }
}
