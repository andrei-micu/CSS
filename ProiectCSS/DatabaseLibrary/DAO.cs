using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace DatabaseLibrary
{
    public class DAO : DatabaseLibrary.IDAO
    {
        private string APPLICANTS_FILE = "applicants.txt";
        private string RESULTS_FILE = "results.txt";

        public void clearApplicants()
        {
            checkCreateFile(APPLICANTS_FILE);
            File.WriteAllText(APPLICANTS_FILE, String.Empty);
        }

        public void clearResults()
        {
            checkCreateFile(RESULTS_FILE);
            File.WriteAllText(RESULTS_FILE, String.Empty);
        }

        public void insertApplicant(IApplicant applicant)
        {
            /* assertion that the applicant contains proper fields */
            checkApplicantPreconditions(applicant);

            /* assertion that the path for the file was not modified */
            Debug.Assert(APPLICANTS_FILE.Equals("applicants.txt"), "The path for the file was changed without permission");
            checkCreateFile(APPLICANTS_FILE);
            /*assertion that the file should exist at this time*/
            Debug.Assert(File.Exists(APPLICANTS_FILE) == true, "The file should have been created!");

            string[] applicantInfo = { applicant.Cnp, applicant.FirstName, applicant.LastName, applicant.FatherInitial, applicant.City, 
                                       applicant.Locality, applicant.SchoolName, applicant.TestMark.ToString(), applicant.AvgExamen.ToString(), 
                                       applicant.DomainMark.ToString(), applicant.GeneralAverage.ToString() };
            List<string> finalApplicants;
            List<string> allApplicants = File.ReadAllLines(APPLICANTS_FILE).ToList();
            finalApplicants = allApplicants.Where(x => x.IndexOf(applicant.Cnp) < 0).ToList();
            if (finalApplicants.Count == allApplicants.Count)
            {
                /* recheck that applicant is not already inserted */
                Debug.Assert(allApplicants.Where(x => x.IndexOf(applicant.Cnp) >= 0).ToList().Count == 0, "Trying to insert a applicant that already exists!");
                File.AppendAllText(APPLICANTS_FILE, string.Join(",", applicantInfo) + Environment.NewLine);
            }
            else
            {
                /* recheck that a new applicant should be inserted */
                Debug.Assert(finalApplicants.Count < allApplicants.Count, "New applicant doesn't exists, but is not added!");
                Console.WriteLine("The applicant already exists!");
            }
        }

        public void deleteApplicant(string cnp)
        {
            /* precondition assertion on CNP format */
            Debug.Assert(cnp.Length == 13, "CNP format not respected!");

            /* assertions for file, it should have the same path and should be created if it doesn't exist when deletion is called */
            Debug.Assert(APPLICANTS_FILE.Equals("applicants.txt"), "The path for the file was changed without permission");
            checkCreateFile(APPLICANTS_FILE);
            Debug.Assert(File.Exists(APPLICANTS_FILE) == true, "The file should have been created!");


            List<string> finalApplicants;

            List<string> allApplicants = File.ReadAllLines(APPLICANTS_FILE).ToList();
            /* assertion regarding the length of the applicants list*/
            Debug.Assert(allApplicants.Count == File.ReadLines(APPLICANTS_FILE).Count(), "The length of the applicants should be the same as lines in the file");
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

        public void updateApplicant(string cnp, IApplicant applicant)
        {
            /* precondition assertion on CNP format */
            Debug.Assert(cnp.Length == 13, "CNP format not respected!");

            /* assertion that the applicant contains proper fields */
            checkApplicantPreconditions(applicant);

            /* assertion the the path for the file was not modified */
            Debug.Assert(APPLICANTS_FILE.Equals("applicants.txt"), "The path for the file was changed without permission");
            checkCreateFile(APPLICANTS_FILE);
            /* assertion that the file should exist at this time */
            Debug.Assert(File.Exists(APPLICANTS_FILE) == true, "The file should have been created!");

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

        public void insertResults(IList<IResult> results)
        {
            /* assertion the the path for the file was not modified */
            Debug.Assert(RESULTS_FILE.Equals("results.txt"), "The path for the file was changed without permission");
            checkCreateFile(RESULTS_FILE);
            /* assertion that the file should exist at this time */
            Debug.Assert(File.Exists(RESULTS_FILE) == true, "The file should have been created!");

            foreach (IResult result in results)
            {
                /* assertion that the result has proper values */
                Debug.Assert(result != null, "Result entry must not be null");
                Debug.Assert(result.result != null, "Result result must not be null");
                IApplicant applicant = result.applicant;
                checkApplicantPreconditions(applicant);

                string[] applicantInfo = { applicant.Cnp, applicant.FirstName, applicant.LastName, applicant.FatherInitial, applicant.City, 
                                           applicant.Locality, applicant.SchoolName, applicant.TestMark.ToString(), applicant.AvgExamen.ToString(), 
                                           applicant.DomainMark.ToString(), applicant.GeneralAverage.ToString(), result.result};
                File.AppendAllText(RESULTS_FILE, string.Join(",", applicantInfo) + Environment.NewLine);
            }
        }

        public IList<IApplicant> getApplicants()
        {
            checkCreateFile(APPLICANTS_FILE);

            IList<IApplicant> applicants = new List<IApplicant>();
            string applicantRow;
            StreamReader streamReader = new StreamReader(APPLICANTS_FILE);
            try
            {
                using (streamReader)
                {
                    while ((applicantRow = streamReader.ReadLine()) != null)
                    {
                        string[] applicantInfo = applicantRow.Split(',');
                        IApplicant applicant = new Applicant(applicantInfo[0], applicantInfo[1], applicantInfo[2], applicantInfo[3], applicantInfo[4], applicantInfo[5],
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


        public IList<IResult> getResultsByType(string type)
        {
            checkCreateFile(RESULTS_FILE);
            IList<IResult> results = getResults();
            IList<IResult> resultsByType = new List<IResult>();
            foreach (IResult result in results)
            {
                if (result.result == type)
                {
                    resultsByType.Add(result);
                }
            }
            return resultsByType;
        }

        public IList<IResult> getResults()
        {
            checkCreateFile(RESULTS_FILE);
            IList<IResult> results = new List<IResult>();
            string line;
            StreamReader streamReader = new StreamReader(RESULTS_FILE);
            try
            {
                using (streamReader)
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] applicantInfo = line.Split(',');
                        IApplicant applicant = new Applicant(applicantInfo[0], applicantInfo[1], applicantInfo[2], applicantInfo[3], applicantInfo[4], applicantInfo[5],
                                              applicantInfo[6], Convert.ToDouble(applicantInfo[7]), Convert.ToDouble(applicantInfo[8]), Convert.ToDouble(applicantInfo[9]));
                        applicant.GeneralAverage = Convert.ToDouble(applicantInfo[10]);
                        String resultString = applicantInfo[11];
                        IResult result = new Result(applicant, resultString);
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
            Debug.Assert(fileName != null);
            Debug.Assert(fileName != "");
            FileStream fs = null;
            if (!File.Exists(fileName))
            {
                fs = File.Create(fileName);
                fs.Close();
            }
        }


        public void checkApplicantPreconditions(IApplicant applicant)
        {
            /* assertion for precondition applicant */
            Debug.Assert(applicant != null, "Applicant should not be null");
            Debug.Assert(applicant.GeneralAverage >= 1.0 && applicant.GeneralAverage <= 10.0);
            Debug.Assert(applicant.TestMark >= 1.0 && applicant.TestMark <= 10.0);
            Debug.Assert(applicant.DomainMark >= 1.0 && applicant.DomainMark <= 10.0);
            Debug.Assert(applicant.AvgExamen >= 1.0 && applicant.AvgExamen <= 10.0);
            Debug.Assert(applicant.FatherInitial.Length == 2);
            Debug.Assert(applicant.FirstName.Length > 2);
            Debug.Assert(applicant.LastName.Length > 2);
            Debug.Assert(applicant.Locality.Length > 2);
            Debug.Assert(applicant.Cnp.Length == 13);
        }

    }
}
