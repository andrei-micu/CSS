using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;

namespace AdmissionLibrary
{
    public class Admission : AdmissionLibrary.IAdmission
    {
        private IDAO dao;
        private const int BUGET_PLACES = 1;//100;
        private const int TAX_PLACES = 5;//150;
        private const string BUGET = "ADMIS/BUGET";
        private const string TAX = "ADMIS/TAXA";
        private const string REJECT = "RESPINS";

        public Admission(IDAO dao)
        {
            this.dao = dao;
        }

        /* --- Introducerea datelor --- */

        public void insertApplicant(IApplicant applicant)
        {
            if (applicant == null)
                return;
            applicant.GeneralAverage = calculateAverage(applicant);
            dao.insertApplicant(applicant);             
        }

        public void deleteApplicant(string cnp)
        {
            dao.deleteApplicant(cnp);
        }

        public void updateApplicant(string cnp, IApplicant applicant)
        {
            if (applicant == null)
                return;
            applicant.GeneralAverage = calculateAverage(applicant);
            dao.updateApplicant(cnp, applicant);
        }


        /* --- Calcularea mediilor de admitere --- */

        private double calculateAverage(IApplicant applicant)
        {
            double average = applicant.TestMark * 0.5 + applicant.DomainMark * 0.3 + applicant.AvgExamen * 0.2;
            return average;        
            
        }


        /* --- Calcularea si publicarea rezultatelor admiterii --- */

        public void calculateAndPublishResults()
        {
            IList<IApplicant> applicants = dao.getApplicants();
            if (applicants.Count == 0)
                return;

            IList<IResult> results = new List<IResult>();
            setTop(applicants);
            int index = 0;
            while (index < BUGET_PLACES && index < applicants.Count)
            {
                results.Add(new Result(applicants.ElementAt(index), BUGET));
                index++;
            }
            while (index < BUGET_PLACES + TAX_PLACES && index < applicants.Count)
            {
                results.Add(new Result(applicants.ElementAt(index), TAX));
                index++;
            }
            while (index < applicants.Count)
            {
                results.Add(new Result(applicants.ElementAt(index), REJECT));
                index++;
            }
            dao.clearResults();
            dao.insertResults(results);
            
        }

        private void setTop(IList<IApplicant> applicants)
        {
            ((List<IApplicant>)applicants).Sort();
        }

        /* --- Popularea bazei de date cu date de test --- */

        public void populateDB()
        {
            dao.clearApplicants();

            insertApplicant(new Applicant("2900610155203", "Irina", "Naum", "V.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.70, 9.30, 10.0));
            insertApplicant(new Applicant("2891245633221", "Dana", "Harbon", "A.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 7.90, 8.30, 9.50));
            insertApplicant(new Applicant("2904533456878", "Corina", "Micla", "T.", "Iasi", "Iasi", "Colegiul National", 9.20, 9.40, 10.0));
            insertApplicant(new Applicant("1891212566544", "Andrei", "Juve", "V.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.70, 7.80, 10.0));
            insertApplicant(new Applicant("1901009778987", "Serban", "Donise", "B.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 6.45, 9.00, 9.50));
            insertApplicant(new Applicant("1900808776443", "Tudor", "Rent", "R.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.40, 6.30, 10.0));
            insertApplicant(new Applicant("2891023990887", "Renata", "Paun", "A.", "Iasi", "Iasi", "Colegiul National Costache Negruzzi", 9.90, 6.30, 9.50));
            insertApplicant(new Applicant("1890306789086", "George", "Cojun", "T.", "Iasi", "Iasi", "Colegiul National Emil Racovita", 8.45, 8.90, 9.30));
            insertApplicant(new Applicant("2900212987654", "Laura", "Leonte", "M.", "Iasi", "Iasi", "Liceul de Informatica Grigore Moisil", 9.10, 9.60, 9.80));
            insertApplicant(new Applicant("1890101987432", "Mihai", "Tatar", "V.", "Iasi", "Iasi", "Colegiul National Costache Negruzzi", 8.90, 9.60, 9.70));


        }
    }
}
