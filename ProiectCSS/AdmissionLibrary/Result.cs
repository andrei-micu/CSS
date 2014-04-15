using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdmissionLibrary
{
    public class Result
    {
        public Applicant applicant {get; set;}
        public string result { get; set; }

        public Result(Applicant app, string result)
        {
            this.applicant = app;
            this.result = result;
        }

    }
}
