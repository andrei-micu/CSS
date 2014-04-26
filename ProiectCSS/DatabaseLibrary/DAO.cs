using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DatabaseLibrary
{
    public class DAO
    {
        private string APPLICANTS_FILE = "applicants.txt";
        private string RESULTS_FILE = "results.txt";

        public string GetHelloWorldFromDB()
        {
            return "Hello World from Database!";
        }

        public void insertApplicants()
        {
            checkCreateFile(APPLICANTS_FILE);
            File.WriteAllText(APPLICANTS_FILE, String.Empty);
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


        public void insertApplicant(Applicant applicant)
        {
            string[] applicantInfo = { applicant.Cnp, applicant.FirstName, applicant.LastName, applicant.FatherInitial, applicant.City, 
                                       applicant.Locality, applicant.SchoolName, applicant.TestMark.ToString(), applicant.AvgExamen.ToString(), 
                                       applicant.DomainMark.ToString(), applicant.GeneralAverage.ToString() };
            List<string> finalApplicants;
            List<string> allApplicants = File.ReadAllLines(APPLICANTS_FILE).ToList();
            finalApplicants = allApplicants.Where(x => x.IndexOf(applicant.Cnp) < 0).ToList();
            if (finalApplicants.Count == allApplicants.Count)
            {
                File.AppendAllText(APPLICANTS_FILE, string.Join(",", applicantInfo) + Environment.NewLine);
            }
            else
            {
                Console.WriteLine("The applicant already exists!");
            }
        }

        public void deleteApplicant(string cnp)
        {
            List<string> finalApplicants;
            List<string> allApplicants = File.ReadAllLines(APPLICANTS_FILE).ToList();
            finalApplicants = allApplicants.Where(x => x.IndexOf(cnp) < 0).ToList();
            if (finalApplicants.Count < allApplicants.Count)
            {
                File.WriteAllLines(APPLICANTS_FILE, finalApplicants.ToArray());
            }
            else
            {
                Console.WriteLine("The applicant does not exist!");
            }
        }

        public void updateApplicant(string cnp, Applicant applicant)
        {
            string[] applicantInfo = { applicant.Cnp, applicant.FirstName, applicant.LastName, applicant.FatherInitial, applicant.City, 
                                       applicant.Locality, applicant.SchoolName, applicant.TestMark.ToString(), applicant.AvgExamen.ToString(), 
                                       applicant.DomainMark.ToString(), applicant.GeneralAverage.ToString() };
            List<string> finalApplicants;
            List<string> allApplicants = File.ReadAllLines(APPLICANTS_FILE).ToList();
            finalApplicants = allApplicants.Where(x => x.IndexOf(cnp) < 0).ToList();
            if (finalApplicants.Count < allApplicants.Count)
            {
                finalApplicants.Add(string.Join(",", applicantInfo));
                File.WriteAllLines(APPLICANTS_FILE, finalApplicants.ToArray());
            }
            else
            {
                Console.WriteLine("The applicant does not exist!");
            }
        }

        public void insertResults(List<Result> results)
        {
            checkCreateFile(RESULTS_FILE);
            File.WriteAllText(RESULTS_FILE, String.Empty);
            foreach (Result result in results)
            {
                Applicant applicant = result.applicant;
                string[] applicantInfo = { applicant.Cnp, applicant.FirstName, applicant.LastName, applicant.FatherInitial, applicant.City, 
                                           applicant.Locality, applicant.SchoolName, applicant.TestMark.ToString(), applicant.AvgExamen.ToString(), 
                                           applicant.DomainMark.ToString(), applicant.GeneralAverage.ToString(), result.result};
                File.AppendAllText(RESULTS_FILE, string.Join(",", applicantInfo) + Environment.NewLine);
            }
        }

        public List<Applicant> getApplicants()
        {
            List<Applicant> applicants = new List<Applicant>();
            string applicantRow;
            StreamReader streamReader = new StreamReader(APPLICANTS_FILE);
            try
            {
                using (streamReader)
                {
                    while ((applicantRow = streamReader.ReadLine()) != null)
                    {
                        string[] applicantInfo = applicantRow.Split(',');
                        Applicant applicant = new Applicant(applicantInfo[0], applicantInfo[1], applicantInfo[2], applicantInfo[3], applicantInfo[4], applicantInfo[5],
                                              applicantInfo[6], Convert.ToDouble(applicantInfo[7]), Convert.ToDouble(applicantInfo[8]), Convert.ToDouble(applicantInfo[9]));
                        applicant.GeneralAverage = Convert.ToDouble(applicantInfo[10]);
                        applicants.Add(applicant);
                    }
                }
                return applicants;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: " + e.Message);
                return null;
            }
            finally
            {
                streamReader.Close();
            }
        }


        public List<Result> getResultsByType(string type)
        {
            checkCreateFile(RESULTS_FILE);
            List<Result> results = getResults();
            List<Result> resultsByType = new List<Result>();
            foreach (Result result in results)
            {
                if (result.result == type)
                {
                    resultsByType.Add(result);
                }
            }
            return resultsByType;
        }

        public List<Result> getResults()
        {
            checkCreateFile(RESULTS_FILE);
            List<Result> results = new List<Result>();
            string line;
            StreamReader streamReader = new StreamReader(RESULTS_FILE);
            try
            {
                using (streamReader)
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] applicantInfo = line.Split(',');
                        Applicant applicant = new Applicant(applicantInfo[0], applicantInfo[1], applicantInfo[2], applicantInfo[3], applicantInfo[4], applicantInfo[5],
                                              applicantInfo[6], Convert.ToDouble(applicantInfo[7]), Convert.ToDouble(applicantInfo[8]), Convert.ToDouble(applicantInfo[9]));
                        applicant.GeneralAverage = Convert.ToDouble(applicantInfo[10]);
                        String resultString = applicantInfo[11];
                        Result result = new Result(applicant, resultString);
                        results.Add(result);
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: " + e.Message);
                return null;
            }
            finally
            {
                streamReader.Close();
            }
        }


        public void checkCreateFile(string fileName)
        {
            FileStream fs = null;
            if (!File.Exists(fileName))
            {
                fs = File.Create(fileName);
                fs.Close();
            }
        }
  
    }
}
