﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;
using AdmissionLibrary;

namespace ForTest
{
    class Program
    {
        private void Run()
        {
            DAO dao = new DAO();
            Admission admission = new Admission(dao);
          
            //Console.WriteLine(dao.GetHelloWorldFromDB());
            //Console.WriteLine(admission.GetHelloWorldFromAdmission());
            
            //-----------------------------------

            admission.populateDB();

            //test get applicants
            //List<Applicant> applicants = dao.getApplicants();
            //foreach (Applicant applicant in applicants)
            //{
            //    Console.WriteLine(applicant.FirstName + " " + applicant.LastName + " " + applicant.GeneralAverage);
            //}

            //publish results from db
            //List<Result> results = admission.publishResults();
            //foreach (Result result in results)
            //{
            //    Console.WriteLine(result.applicant.FirstName + " " + result.applicant.LastName + " " + result.result);
            //}

            //test results by type
            //List<Result> results = dao.getResultsByType("RESPINS");
            //foreach (Result result in results)
            //{
            //    Console.WriteLine(result.applicant.FirstName + " " + result.applicant.LastName + " " + result.result);
            //}

            //test insert
            admission.insertApplicant(new Applicant("2900680155208", "Alexandra", "Miron", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));

            //test delete
            dao.deleteApplicant("1891212566544");

            //test update
            dao.updateApplicant("2900212987654", new Applicant("2900212987654", "Ioana", "Leonte", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));

            //test get applicants and set results
            admission.calculateAndPublishResults();
            IList<IResult> results = dao.getResults();
            foreach(IResult result in results)
            {
                Console.WriteLine(result.applicant.Cnp + " " + result.result);
            }

            IList<IResult> rejects = dao.getResultsByType("RESPINS");
            Console.WriteLine();
            foreach (IResult result in rejects)
            {
                Console.WriteLine(result.applicant.Cnp + " " + result.result);
            }
            
        }

        static void Main(string[] args)
        {
            new Program().Run();
            Console.Read();
        }



   
    }
}
