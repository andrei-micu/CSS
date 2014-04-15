using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;
using AdmissionLibrary;

namespace ForTest
{
    class Program
    {
        private DAO dao = new DAO();
        private Admission admission = new Admission();

        private void Run()
        {
          
            Console.WriteLine(dao.GetHelloWorldFromDB());
            Console.WriteLine(admission.GetHelloWorldFromAdmission());
            admission.test();
        }

        static void Main(string[] args)
        {
            new Program().Run();
            Console.Read();
        }



   
    }
}
