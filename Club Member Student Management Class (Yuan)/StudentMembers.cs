using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_StudentClubManager__Midterms_
{
    /// <summary>
    /// handles the data related to student members 
    /// </summary>
    public class StudentMember
    {
        // Fields 
        private string StudentID;
        // MemberName[0] = LastName, MemberName[1] = FirstName
        private string[] MemberName = new string[2];
        private string FullName;

        // Constructor to initialize fields
        public StudentMember(string StudentId, string FirstName, string LastName, string Fullname)
        {
            this.StudentID = StudentId;
            MemberName[0] = LastName;
            MemberName[1] = FirstName;
            this.FullName = Fullname;
        }

        public string getStudentID()
        {
            return StudentID;
        }

        public string getFirstName()
        {
            return MemberName[1];
        }

        public string getLastName()
        {
            return MemberName[0];
        }

      
        // Returns the full name by combining the first and last names.
        public string getFullName()
        {
            return getFirstName() + " " + getLastName();
        }
    }
}
