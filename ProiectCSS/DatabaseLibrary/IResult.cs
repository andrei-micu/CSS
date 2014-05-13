using System;
namespace DatabaseLibrary
{
    public interface IResult
    {
        IApplicant applicant { get; set; }
        string result { get; set; }
    }
}
