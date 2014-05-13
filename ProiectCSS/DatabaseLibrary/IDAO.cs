using System;
namespace DatabaseLibrary
{
    public interface IDAO
    {
        void checkCreateFile(string fileName);
        void clearApplicants();
        void clearResults();
        void deleteApplicant(string cnp);
        System.Collections.Generic.IList<IApplicant> getApplicants();
        System.Collections.Generic.IList<IResult> getResults();
        System.Collections.Generic.IList<IResult> getResultsByType(string type);
        void insertApplicant(IApplicant applicant);
        void insertResults(System.Collections.Generic.IList<IResult> results);
        void updateApplicant(string cnp, IApplicant applicant);
    }
}
