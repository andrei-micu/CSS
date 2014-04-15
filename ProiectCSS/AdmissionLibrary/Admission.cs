using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;

namespace AdmissionLibrary
{
    public class Admission
    {
        private DAO dao = new DAO();
        private int BUGET_PLACES = 1;//100;
        private int TAX_PLACES = 5;//150;
        private int ENTRY_NUMBER = 0;


        public string GetHelloWorldFromAdmission()
        {
            return "Hello World from Admission module!";
        }


        public string generateNextIdentifier()
        {
            ENTRY_NUMBER++;
            return ("EN" + ENTRY_NUMBER);
        }

        public Applicant insertApplicant(Applicant applicant)
        {
            applicant.GeneralAverage = calculateAverage(applicant);
            applicant.Identifier = generateNextIdentifier();
            return applicant;
            //dao.insertApplicant(applicant);
        }

        public double calculateAverage(Applicant applicant)
        {
            double average = applicant.TestMark * 0.5 + applicant.DomainMark * 0.3 + applicant.AvgExamen * 0.2;
            return average;
        }

        public void setTop(List<Applicant> applicants)
        {
            applicants.Sort();
        }

        public void setResults()
        {
            List<Applicant> applicants = null; //dao.getApplicants();
            List<Result> results = new List<Result>();
            setTop(applicants);
            int index = 0;
            while (index < BUGET_PLACES)
            {
                results.Add(new Result(applicants.ElementAt(index), "ADMIS/BUGET"));
                index++;
            }
            while (index < BUGET_PLACES + TAX_PLACES)
            {
                results.Add(new Result(applicants.ElementAt(index), "ADMIS/TAXA"));
                index++;
            }
            while (index < applicants.Count)
            {
                results.Add(new Result(applicants.ElementAt(index), "RESPINS"));
                index++;
            }
            //dao.insertResults(results);
           
        }

        public List<Result> publishResults()
        {
            List<Result> results = null;//dao.getResult();
            return results;
        }

        public void test()
        {
            List<Applicant> applicants = populateApplicantList();
            setResultsTest(applicants);
        }

        public List<Applicant> populateApplicantList()
        {

            List<Applicant> applicants = new List<Applicant>();
            applicants.Add(insertApplicant(new Applicant("Irina", "Naum", "V.", "Iasi", "Iasi", 8.70, 9.30, 10.0)));
            applicants.Add(insertApplicant(new Applicant("Dana", "Harbon", "A.", "Iasi", "Iasi", 7.90, 8.30, 9.50)));
            applicants.Add(insertApplicant(new Applicant("Corina", "Micla", "T.", "Iasi", "Iasi", 9.20, 9.40, 10.0)));
            applicants.Add(insertApplicant(new Applicant("Andrei", "Juve", "V.", "Iasi", "Iasi", 9.70, 7.80, 10.0)));
            applicants.Add(insertApplicant(new Applicant("Serban", "Donise", "B.", "Iasi", "Iasi", 6.45, 9.00, 9.50)));
            applicants.Add(insertApplicant(new Applicant("Tudor", "Rent", "R.", "Iasi", "Iasi", 9.40, 6.30, 10.0)));
            applicants.Add(insertApplicant(new Applicant("Renata", "Paun", "A.", "Iasi", "Iasi", 9.90, 6.30, 9.50)));
            applicants.Add(insertApplicant(new Applicant("George", "Cojun", "T.", "Iasi", "Iasi", 8.45, 8.90, 9.30)));
            applicants.Add(insertApplicant(new Applicant("Laura", "Leonte", "M.", "Iasi", "Iasi", 9.10, 9.60, 9.80)));
            applicants.Add(insertApplicant(new Applicant("Mihai", "Tatar", "V.", "Iasi", "Iasi", 8.90, 9.60, 9.70)));

            

            return applicants;
        }

        public void setResultsTest(List<Applicant> applicants)
        {
            List<Result> results = new List<Result>();
            setTop(applicants);
            int index = 0;
            while (index < BUGET_PLACES)
            {
                results.Add(new Result(applicants.ElementAt(index), "ADMIS/BUGET"));
                index++;
            }
            while (index < BUGET_PLACES + TAX_PLACES)
            {
                results.Add(new Result(applicants.ElementAt(index), "ADMIS/TAXA"));
                index++;
            }
            while (index < applicants.Count)
            {
                results.Add(new Result(applicants.ElementAt(index), "RESPINS"));
                index++;
            }
            //dao.insertResults(results);
            publishResultsTest(results);
        }


        public void publishResultsTest(List<Result> results)
        {
            foreach (Result result in results)
            {
                Console.WriteLine(result.applicant.Identifier + " - " + result.applicant.FirstName + " - " +
                                    result.applicant.LastName + " - " + result.applicant.GeneralAverage + " - " +
                                    result.result);
            }
        }

         
    }
}
