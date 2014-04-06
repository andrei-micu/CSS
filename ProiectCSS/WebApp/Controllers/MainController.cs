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
        private DAO dao = new DAO();
        private Admission admission = new Admission();

        //
        // GET: /Main/

        public ActionResult Index()
        {
            ViewBag.MessageDB = dao.GetHelloWorldFromDB();
            ViewBag.MessageAdmission = admission.GetHelloWorldFromAdmission();
            return View();
        }

    }
}
