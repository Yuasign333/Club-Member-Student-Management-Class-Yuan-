using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_StudentClubManager__Midterms_
{
    /// <summary>
    /// Handles the data related to student clubs + their members and administrators
    /// </summary>
    public class Clubs
    {
        // Private fields to hold the data
        private string clubID;
        private string clubName;
        private ClubAdmin admin; // from club admin class

        private List<StudentMember> members; // List to hold student members (from student member class)
       

        // Constructor to initialize the private fields
        public Clubs(string clubID, string clubName, ClubAdmin admin)
        {
            this.clubID = clubID;
            this.clubName = clubName;
            this.admin = admin;
           
            members = new List<StudentMember>(); // List is initialized 
        }

        // Getters
        public string GetClubID()
        {
            return clubID;
        }

        public string GetClubName()
        {
            return clubName;
        }

      
        public ClubAdmin GetAdministrator()
        {
            return admin;
        }

        // Getter for the list of members (needed by ClubRegistry)
        public List<StudentMember> GetMembers()
        {
            return members;
        }
      
    }
}
