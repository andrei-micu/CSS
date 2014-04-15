﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdmissionLibrary
{
    public class Applicant: IComparable<Applicant>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherInitial { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string SchoolName { get; set; }
        public string Identifier { get; set; }
        public double TestMark { get; set; }
        public double AvgExamen { get; set; }
        public double DomainMark { get; set; }
        public double GeneralAverage { get; set; }
       
        public Applicant(string firstName, string lastName, string fatherInitial, string city, string locality,
                         double testMark, double avgExamen, double domainMark)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FatherInitial = fatherInitial;
            this.City = city;
            this.Locality = locality;
            this.TestMark = testMark;
            this.AvgExamen = avgExamen;
            this.DomainMark = domainMark;


            double average = this.TestMark * 0.5 + this.DomainMark * 0.3 + this.AvgExamen * 0.2;
            this.GeneralAverage = average;
        }

        public int CompareTo(Applicant applicant)
        {
            if (this.GeneralAverage == applicant.GeneralAverage)
            {
                if (this.TestMark == applicant.TestMark)
                {
                    return applicant.AvgExamen.CompareTo(this.AvgExamen);
                }

                return applicant.TestMark.CompareTo(this.TestMark);
            }

            return applicant.GeneralAverage.CompareTo(this.GeneralAverage);
        }

        
    }
}