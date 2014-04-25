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


        public string GetHelloWorldFromAdmission()
        {
            return "Hello World from Admission module!";
        }

        public Applicant insertApplicant(Applicant applicant)
        {
            applicant.GeneralAverage = calculateAverage(applicant);
            dao.insertApplicant(applicant);
            return applicant;
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
            List<Applicant> applicants = dao.getApplicants();
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
            dao.insertResults(results);
           
        }

        public List<Result> publishResults()
        {
            List<Result> results = dao.getResults();
            return results;
        }


    }
}
