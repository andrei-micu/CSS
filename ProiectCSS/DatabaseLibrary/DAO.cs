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

        public void insertApplicant(Applicant applicant)
        {
            checkCreateFile(APPLICANTS_FILE);
            string[] applicantInfo = { applicant.Cnp, applicant.FirstName, applicant.LastName, applicant.FatherInitial, applicant.City, 
                                       applicant.Locality, applicant.SchoolName, applicant.TestMark.ToString(), applicant.AvgExamen.ToString(), 
                                       applicant.DomainMark.ToString(), applicant.GeneralAverage.ToString() };
            List<string> finalApplicants;
            List<string> allApplicants = File.ReadAllLines(APPLICANTS_FILE).ToList();
            finalApplicants = allApplicants.Where(x => x.IndexOf(applicant.Cnp) < 0).ToList();
            if (finalApplicants.Count == allApplicants.Count)
            {
                File.AppendAllText(APPLICANTS_FILE, Environment.NewLine + string.Join(",", applicantInfo));
            }
            else
            {
                Console.WriteLine("The applicant already exists!");
            }
        }

        public void deleteApplicant(string cnp)
        {
            checkCreateFile(APPLICANTS_FILE);
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
            checkCreateFile(APPLICANTS_FILE);
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
            checkCreateFile(APPLICANTS_FILE);
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
