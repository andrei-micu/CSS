using System;
namespace DatabaseLibrary
{
    public interface IApplicant : IComparable<IApplicant>
    {
        double AvgExamen { get; set; }
        string City { get; set; }
        string Cnp { get; set; }
        new int CompareTo(IApplicant applicant);
        double DomainMark { get; set; }
        string FatherInitial { get; set; }
        string FirstName { get; set; }
        double GeneralAverage { get; set; }
        string LastName { get; set; }
        string Locality { get; set; }
        string SchoolName { get; set; }
        double TestMark { get; set; }
    }
}
