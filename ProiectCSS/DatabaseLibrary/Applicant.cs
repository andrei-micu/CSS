using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLibrary
{
    public class Applicant: DatabaseLibrary.IApplicant
    {
        private string _Cnp;
        private string _FirstName;
        private string _LastName;
        private string _FatherInitial;
        private string _City;
        private string _Locality;
        private string _SchoolName;
        private double _TestMark;
        private double _AvgExamen;
        private double _DomainMark;
        private double _GeneralAverage;

        [Required(ErrorMessage = "Invalid format for Cnp")]
        [StringLength(13, MinimumLength = 13)]
        [RegularExpression("^[0-9]+$")]
        public string Cnp
        {
            get 
            {
                return _Cnp;
            }

            set
            {
                
                    Validator.ValidateProperty(value,
                        new ValidationContext(this, null, null) { MemberName = "Cnp" });
                    _Cnp = value;
               
            }
        }

        [Required(ErrorMessage = "Invalid format for first name")]
        [StringLength(maximumLength: 15, MinimumLength = 3)]
        [RegularExpression("^[A-Z][a-z]+$")]
        public string FirstName
        {
            get
            {
                return _FirstName;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "FirstName" });
                _FirstName = value;

            }
        }

        [Required(ErrorMessage = "Invalid format for last name")]
        [StringLength(maximumLength: 15, MinimumLength = 3)]
        [RegularExpression("^[A-Z][a-z]+$")]
        public string LastName
        {
            get
            {
                return _LastName;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "LastName" });
                _LastName = value;

            }
        }


        [Required(ErrorMessage = "Invalid format for father initial")]
        [StringLength(2)]
        [RegularExpression("^[A-Z].$")]
        public string FatherInitial
        {
            get
            {
                return _FatherInitial;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "FatherInitial" });
                _FatherInitial = value;

            }
        }

        [Required(ErrorMessage = "Invalid format for city")]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        [RegularExpression("^[A-Z][a-z]+$")]
        public string City
        {
            get
            {
                return _City;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "City" });
                _City = value;

            }
        }

        [Required(ErrorMessage = "Invalid format for locality")]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        [RegularExpression("^[A-Z][a-z]+$")]
        public string Locality
        {
            get
            {
                return _Locality;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "Locality" });
                _Locality = value;

            }
        }

        [Required(ErrorMessage = "Invalid format for school name")]
        public string SchoolName
        {
            get
            {
                return _SchoolName;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "SchoolName" });
                _SchoolName = value;

            }
        }

        [Required(ErrorMessage = "Invalid format for test mark")]
        [Range(1.0, 10.0)]
        public double TestMark
        {
            get
            {
                return _TestMark;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "TestMark" });
                _TestMark = value;

            }
        }

        [Required(ErrorMessage = "Invalid format for average exam")]
        [Range(1.0, 10.0)]
        public double AvgExamen
        {
            get
            {
                return _AvgExamen;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "AvgExamen" });
                _AvgExamen = value;

            }
        }

        [Required(ErrorMessage = "Invalid format for domain mark")]
        [Range(1.0, 10.0)]
        public double DomainMark
        {
            get
            {
                return _DomainMark;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "DomainMark" });
                _DomainMark = value;

            }
        }

        [Required(ErrorMessage = "Invalid format for general average")]
        [Range(1.0, 10.0)]
        public double GeneralAverage
        {
            get
            {
                return _GeneralAverage;
            }

            set
            {

                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "GeneralAverage" });
                _GeneralAverage = value;

            }
        }



        public Applicant()
        {

        }
       
        public Applicant(string cnp, string firstName, string lastName, string fatherInitial, string city, string locality, 
                         string schoolName, double testMark, double avgExamen, double domainMark)
        {
            this.Cnp = cnp;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FatherInitial = fatherInitial;
            this.City = city;
            this.Locality = locality;
            this.SchoolName = schoolName;
            this.TestMark = testMark;
            this.AvgExamen = avgExamen;
            this.DomainMark = domainMark;

            double average = this.TestMark * 0.5 + this.DomainMark * 0.3 + this.AvgExamen * 0.2;
            this.GeneralAverage = average;
        }

        public int CompareTo(IApplicant applicant)
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

        
        public override bool Equals(object obj)
        {
            Applicant applicant = (Applicant)obj;
            if (applicant == null)
                return false;
            if (applicant.Cnp != this.Cnp)
                return false;
            if (applicant.FirstName != this.FirstName)
                return false;
            if (applicant.LastName != this.LastName)
                return false;
            if (applicant.FatherInitial != this.FatherInitial)
                return false;
            if (applicant.City != this.City)
                return false;
            if (applicant.Locality != this.Locality)
                return false;
            if (applicant.SchoolName != this.SchoolName)
                return false;
            if (applicant.TestMark != this.TestMark)
                return false;
            if (applicant.DomainMark != this.DomainMark)
                return false;
            if (applicant.AvgExamen != this.AvgExamen)
                return false;
            if (applicant.GeneralAverage != this.GeneralAverage)
                return false;
            return true;
        }
       
    }
}
