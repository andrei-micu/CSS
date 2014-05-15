using System;
namespace DatabaseLibrary
{
    public interface IApplicant : IComparable<IApplicant>
    {
        string Cnp { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FatherInitial { get; set; }
        string City { get; set; }
        string Locality { get; set; }
        string SchoolName { get; set; }
        double TestMark { get; set; }
        double AvgExamen { get; set; }
        double DomainMark { get; set; }
        double GeneralAverage { get; set; }
        new int CompareTo(IApplicant applicant); 
    }
}
