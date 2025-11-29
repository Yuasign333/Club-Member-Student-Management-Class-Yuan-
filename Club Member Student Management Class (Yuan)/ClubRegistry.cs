using System;
using System.Collections.Generic;

namespace Real_StudentClubManager__Midterms_
{
    /// <summary>
    /// Handles the usage of StudentMembers, ClubAdmins, and Clubs class, and registry of clubs and datas for student enrollments.
    /// </summary>
    public class ClubRegistry
    {
        // fields for club registry class

        private Clubs[] AllClubs; // fixed list of all clubs (from Clubs class)

        private static int StudentIdCounter = 1000; // static because shared across all instances

        // Constructor sets up the fixed data structures
        public ClubRegistry()
        {
            // Get fixed objects for administrators (from ClubAdmin class)
            ClubAdmin adminMusic = new ClubAdmin("A001", "Julian Laspona");
            ClubAdmin adminSports = new ClubAdmin("A002", "Gilbert Clause");
            ClubAdmin adminArt = new ClubAdmin("A003", "Liza Nota");
            ClubAdmin adminBook = new ClubAdmin("A004", "Dean Molde");
            ClubAdmin adminMath = new ClubAdmin("A005", "Rose Tumayan");

            AllClubs = new Clubs[]
            {
                new Clubs("C001", "Music Club", adminMusic),
                new Clubs("C002", "Sports Club", adminSports),
                new Clubs("C003", "Art Club", adminArt),
                new Clubs("C004", "Book Club", adminBook),
                new Clubs("C005", "Math Club", adminMath)
            };
        }

        // Method for data operation upon enrolling a student into a club
        // (Validation logic is in SystemOutput.cs, case 2 under Admin Control)
        public void EnrollStudent(string clubName, string studentName)
        {
            //  Find the target club
            Clubs targetClub = null;
            foreach (Clubs club in AllClubs)
            {
                if (club.GetClubName() == clubName)
                {
                    targetClub = club;
                    break;
                }
            }

            //  If club found, enroll the student
            if (targetClub != null)
            {
                string newId = "S" + StudentIdCounter++; // Dynamic ID generation

                // Split the name string by space
                string[] nameParts = studentName.Split(' ');

                // Assuming the format is "FirstName LastName" (used for SystemOutput.cs)
                if (nameParts.Length >= 2)
                {
                    string firstName = nameParts[0];
                    string lastName = nameParts[1];
                    string fullName = studentName;

                    // Create and add student ( to be used by SystemOutput.cs)
                    StudentMember newStudent = new StudentMember(newId, firstName, lastName, fullName);
                    targetClub.GetMembers().Add(newStudent);
                }
            }
        }

        // Getter for the array of clubs
        public Clubs[] GetClubs()
        {
            return AllClubs;
        }
    }
}
