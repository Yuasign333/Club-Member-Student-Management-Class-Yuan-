using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Real_StudentClubManager__Midterms_
{
    /// <summary>
    /// Handles the usage of StudentMembers, ClubAdmins, and Clubs class, and registry of clubs and student enrollments.
    /// </summary>
    public class ClubRegistry
    {
        // fields for club registry class

        private Clubs[] AllClubs; // fixed list of all clubs (from Clubs class)

        private static int StudentIdCounter = 1000;

        private static int maxStudentsPerClub = 3;

        // Constructor sets up the fixed data structures
        public ClubRegistry()
        {
            // get fixed objects for administrators

            // from ClubAdmin class
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

        // Method for enrolling a student into a club
        public void EnrollStudent(string clubName, string studentName)
        {
            // 1. Logic to find the target club
            Clubs targetClub = null;

            // loop through AllClubs to find the matching club by name
            foreach (Clubs club in AllClubs)
            {
                if (club.GetClubName() == clubName)
                {
                    targetClub = club;
                    break;
                }
            }

            // 2. Proceed if the club was found
            if (targetClub != null)
            {

                string newId = "S" + StudentIdCounter++;   // Dynamic ID generation


                // Split the name string by space into an array of parts
                string[] nameParts = studentName.Split(' ');
                string firstName;
                string lastName;
                string fullName = studentName;

                // We need AT LEAST two parts to assign a first and last name.
                if (nameParts.Length >= 2)
                {

                    // We STRICTLY limit to the first two parts ( first and last name only)

                    firstName = nameParts[0]; // First part as first name


                    lastName = nameParts[1]; // Second part as last name
                }
                else // nameParts.Length is 1 or 0
                {

                    if (nameParts.Length > 0)
                    {
                        firstName = nameParts[0]; // Assign the only part as first name
                    }
                    else
                    {
                        firstName = "";
                    }
                    lastName = ""; // No last name in this case (This is the last name logic for 1 or 0 words)
                }



                // studentmember now contains (ID, First, Last, FullName)
                StudentMember newStudent = new StudentMember(newId, firstName, lastName, fullName); 

                // Logic to check for duplicates 
                bool alreadyEnrolled = false;

                foreach (StudentMember member in targetClub.GetMembers())
                {
                    // Call the getFullName() method on the member object for comparison.
                    if (member.getFullName() == studentName)
                    {
                        alreadyEnrolled = true;
                        break;
                    }
                }
                 if (!alreadyEnrolled && targetClub.GetMembers().Count < maxStudentsPerClub) // check capacity
                {
                    // Using the getter method GetMembers() to access the list for modification
                    targetClub.GetMembers().Add(newStudent);
                }
                else if (alreadyEnrolled)
                {
                    Console.WriteLine($"\nStudent '{studentName}' is already enrolled in '{clubName}'.");
                }
                else if (targetClub.GetMembers().Count >= maxStudentsPerClub)
                {
                    Console.WriteLine($"\nCannot enroll '{studentName}' in '{clubName}': Club is at maximum capacity.");
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