using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_StudentClubManager__Midterms_;

namespace Club_Member_Student_Management_Class__Yuan_
{
    internal class Program
    {
        /// <summary>
        ///  since we rely on systemOutput.cs on all interface and logical function, main is only to call systemOutput class
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           
            var systemOutput = new SystemOutput(); // create instance of systemOutput class

            systemOutput.RunApplication(); // call the main method for that class to run the program
        }
    }
}
