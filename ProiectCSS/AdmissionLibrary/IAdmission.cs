using System;
namespace AdmissionLibrary
{
    public interface IAdmission
    {
        void calculateAndPublishResults();
        void deleteApplicant(string cnp);
        void insertApplicant(DatabaseLibrary.IApplicant applicant);
        void populateDB();
        void updateApplicant(string cnp, DatabaseLibrary.IApplicant applicant);
    }
}
