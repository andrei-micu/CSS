using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLibrary
{
    public class Result : DatabaseLibrary.IResult
    {
        public IApplicant applicant {get; set;}
        public string result { get; set; }

        public Result(IApplicant app, string result)
        {
            this.applicant = app;
            this.result = result;
        }

        public override bool Equals(object obj)
        {
            Result result = (Result)obj;
            if (!result.applicant.Equals(this.applicant))
                return false;
            if (!result.result.Equals(this.result))
                return false;
            return true;
            //return false;
        }

    }
}
