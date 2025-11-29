using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Real_StudentClubManager__Midterms_
{
    /// <summary>
    /// This serves as the main interface and logic for the Club Management System,
    /// </summary>
    public class SystemOutput
    {
        // important variables and data structures

        // Club Registry instance to manage clubs and students (now an instance field)

        private ClubRegistry clubRegistry = new ClubRegistry();

        // Simple lists for announcements  

        private List<string> clubAnnouncements = new List<string>();

        // Queue for waitlist        
        private Queue<string> waitlistQueue = new Queue<string>(); // Waitlist queue storing entries in format "StudentName|ClubName"
                                                                   // Use Split('|') logic to separate: parts[0] = StudentName, parts[1] = ClubName
        // Admin credentials 
        private string AdminPassword = "Club2025"; // fixed password for all admins

        private Clubs currentAdminClub = null; // get the club of the currently logged-in admin

        // Constructor to run initial setup (for announcements)
        public SystemOutput()
        {
            // Initialize with default announcements for each club
            foreach (var club in clubRegistry.GetClubs())
            {
                clubAnnouncements.Add($"{club.GetClubName()}:[Default] Welcome to the {club.GetClubName()}! Please check back for updates.");
            }
        }

        public void RunApplication() // host method to call all other methods (main method for this class)
        {
            PrintWelcomeScreen();
            MainLoginMenu();
        }

        public void PrintWelcomeScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(35, 8);
            Console.WriteLine("A Club Management System ");

            Console.SetCursorPosition(80, 8);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            Console.WriteLine("                                                                      ");
            Console.WriteLine("                                           mmmm      mm         mm    mm    mmmmmm    ");
            Console.WriteLine("                                          ##  ###    ##         ##    ##    ##   #");
            Console.WriteLine("                                         ##          ##         ##    ##    ##   ##  ");
            Console.WriteLine("                                         ##          ##         ##    ##    #######  ");
            Console.WriteLine("                                         ##m         ##         ##    ##    ##   ##  ");
            Console.WriteLine("                                           ##mmmm#   ##mmmmmm    ##mm##     ##mmmm##  ");
            Console.WriteLine("\n");
            Console.ResetColor();
            Console.WriteLine("                                              -------------------------------------------");
            Console.WriteLine();
            Console.SetCursorPosition(44, 25);
            Console.WriteLine("     Press any key to continue...");
            Console.ReadKey();

            Console.SetCursorPosition(49, 28);
            Console.Write("  Loading");

            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(300);
            }

            Console.Clear();
        }

        public void MainLoginMenu() // main login menu for admin and student
        {
            bool exit = false;
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=====================================");
                Console.WriteLine("        MAIN LOGIN SELECTION         ");
                Console.WriteLine("=====================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n1. Administrator Login");
                Console.WriteLine("\n2. Student Login");
                Console.WriteLine("\n3. Exit Application");
                Console.Write("\nEnter your choice: ");
                Console.ResetColor();

                string choiceInput = Console.ReadLine();
                Console.Clear();

                if (int.TryParse(choiceInput, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AdminLogin(false);
                            AdminMenuLoop();
                            break;
                        case 2:
                            StudentLoginAndMenu();
                            break;
                        case 3:
                            exit = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nExiting Club Management System. Goodbye!");
                            Console.ResetColor();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid choice. Please select 1, 2, or 3.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a number.");
                    Console.ResetColor();
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        public void AdminLogin(bool login) // admin login flow
        {
            login = false;
            Clubs matchingClub = null;

            while (!login)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n-----ADMIN LOGIN PANEL-----");
                Console.ResetColor();

                Console.Write("\nEnter Admin Name: ");
                string userNameInput = Console.ReadLine().Trim();

                Console.Write("\nEnter Password: ");
                string passwordInput = Console.ReadLine();

                matchingClub = null;

                // Find matching admin
                foreach (var club in clubRegistry.GetClubs())
                {
                    if (club.GetAdministrator().AdminName().ToLower() == userNameInput.ToLower())
                    {
                        matchingClub = club;
                        break;
                    }
                }

                // Validate
                if (matchingClub != null && passwordInput == AdminPassword)
                {
                    currentAdminClub = matchingClub;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nLogin successful! Press any key to continue...");
                    Console.ResetColor();
                    Console.ReadKey();
                    login = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWrong Admin Name or Password. Press any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }

            // Loading Animation
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\nWELCOME! {currentAdminClub.GetAdministrator().AdminName()}, you are now accessing the {currentAdminClub.GetClubName()} Manager.");
            Console.ResetColor();

            Console.Write("\nLoading");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.Clear();
        }

        public void AdminMenuLoop() // admin menu loop
        {
            bool exit = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=====================================");
                Console.WriteLine($"    {currentAdminClub.GetClubName().ToUpper()} MANAGEMENT MENU");
                Console.WriteLine("=====================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n1. View My Club Status");
                Console.WriteLine("\n2. Enroll Student in My Club");
                Console.WriteLine("\n3. Drop/Remove Student from My Club");
                Console.WriteLine("\n4. View All Clubs");
                Console.WriteLine("\n5. Check Waitlist & Admit Students");
                Console.WriteLine("\n6. Post Club Announcement");
                Console.WriteLine("\n7. Return to Main Login");
                Console.ResetColor();
                Console.Write("\nEnter your choice: ");

                string choiceInput = Console.ReadLine();

                if (int.TryParse(choiceInput, out int choice))
                {
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            ViewMyClubStatus(); // manage whole club update (report)
                            break;
                        case 2:
                            EnrollStudentMenu(); // tracks joining students (updates)
                            break;
                        case 3:
                            DropStudentMenu(); // tracks removing students(updates)
                            break;
                        case 4:
                            ViewAllClubs(); // produce report of all clubs
                            break;
                        case 5:
                            CheckWaitlistAndAdmit(); // manage waitlist membership queue
                            break;
                        case 6:
                            PostAnnouncementMenu(); // produce report within the club (messages)
                            break;
                        case 7:
                            exit = true;
                            currentAdminClub = null;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nReturning to Main Login...");
                            Console.ResetColor();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid choice. Please select a valid option (1-7).");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a number.");
                    Console.ResetColor();
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        // --- ADMIN OPTION IMPLEMENTATIONS ---

        public void ViewMyClubStatus() // case 1 admin menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=====================================");
            Console.WriteLine("       MY CLUB STATUS");
            Console.WriteLine("=====================================");
            Console.ResetColor();

            // Club Information Table
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n{0,-20} | {1,-30}", "Field", "Value");
            Console.WriteLine("-------------------------------------------------------");
            Console.ResetColor();

            // Club Info
            Console.WriteLine("{0,-20} | {1,-30}", "Club ID", currentAdminClub.GetClubID());
            Console.WriteLine("{0,-20} | {1,-30}", "Club Name", currentAdminClub.GetClubName()); 
            Console.WriteLine("{0,-20} | {1,-30}", "Admin ID", currentAdminClub.GetAdministrator().GetadminID());
            Console.WriteLine("{0,-20} | {1,-30}", "Administrator", currentAdminClub.GetAdministrator().AdminName());
            Console.WriteLine("{0,-20} | {1,-30}", "Max Capacity", "3");
            Console.WriteLine("{0,-20} | {1,-30}", "Current Members", currentAdminClub.GetMembers().Count);

            // Members Table
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=====================================");
            Console.WriteLine("           MEMBERS");
            Console.WriteLine("=====================================");
            Console.ResetColor();

            if (currentAdminClub.GetMembers().Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n{0,-15} | {1,-30}", "Student ID", "Student Name");
                Console.WriteLine("-------------------------------------------------------");
                Console.ResetColor();

                int memberIndex = 1;
                foreach (var member in currentAdminClub.GetMembers())
                {
                    string displayID = "S" + memberIndex.ToString("D3"); // S001, S002, S003...
                    Console.WriteLine("{0,-15} | {1,-30}", displayID, member.getFullName());
                    memberIndex++;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n(No members yet.)");
                Console.ResetColor();
            }

            // Waitlist Table
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=====================================");
            Console.WriteLine("          WAITLIST QUEUE");
            Console.WriteLine("=====================================");
            Console.ResetColor();

            int waitlistCount = 0;
            // First count waitlist entries for this club
            foreach (var entry in waitlistQueue)
            {
                string[] parts = entry.Split('|');
                if (parts.Length == 2 && parts[1] == currentAdminClub.GetClubName()) // validates club name
                {
                    waitlistCount++;
                }
            }

            if (waitlistCount > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n{0,-15} | {1,-30}", "Position", "Student Name");
                Console.WriteLine("-------------------------------------------------------");
                Console.ResetColor();

                int position = 1;
                foreach (var entry in waitlistQueue)
                {
                    string[] parts = entry.Split('|');
                    if (parts.Length == 2 && parts[1] == currentAdminClub.GetClubName()) // validates club name
                    {
                        Console.WriteLine("{0,-15} | {1,-30}", "Q" + position.ToString("D3"), parts[0]);
                        position++;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n(No students in waitlist)");
                Console.ResetColor();
            }


        }

        public void EnrollStudentMenu() // case 2 admin menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Enroll Student in My Club ---");
            Console.ResetColor();

            Console.WriteLine($"\nCurrent Members: {currentAdminClub.GetMembers().Count}/3");

            Console.Write("\nEnter Student Full Name: ");
            string studentName = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(studentName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid name. Please try again.");
                Console.ResetColor();
                return;
            }
            // Validate full name (at least two words)
            string[] nameParts = studentName.Split(' ');
            int wordCount = 0;
            foreach (string part in nameParts)
            {
                if (!string.IsNullOrWhiteSpace(part))
                {
                    wordCount++;
                }
            }

            if (wordCount < 2) // must be at least first and last name
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: Please enter a full name (First Name and Last Name).");
               
                Console.ResetColor();
                return;
            }
            else if (wordCount > 2) // strictly only first and last name allowed
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: Please enter a full name of this format only (First Name and Last Name).");

                Console.ResetColor();
                return;
            }

            // Check if student already in ANY club
            bool alreadyEnrolled = false;

            string enrolledClubName = "";

            foreach (var club in clubRegistry.GetClubs()) // check all clubs
            {
                foreach (var member in club.GetMembers()) // check members
                {
                    if (member.getFullName().ToLower() == studentName.ToLower()) // match found
                    {
                        alreadyEnrolled = true;
                        enrolledClubName = club.GetClubName();
                        break;
                    }
                }
                if (alreadyEnrolled)
                {
                    break;
                }
            }

            if (alreadyEnrolled)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{studentName} is already enrolled in {enrolledClubName}.");
                Console.WriteLine("\nStudents can only join one club at a time.");
                Console.ResetColor();
                return;
            }

            // Check if club is full
            if (currentAdminClub.GetMembers().Count >= 3)
            {
                // Enqueue to waitlist
                string waitlistEntry = $"{studentName}|{currentAdminClub.GetClubName()}";

                bool alreadyInWaitlist = false;
                foreach (var entry in waitlistQueue) // check if already in waitlist
                {
                    if (entry == waitlistEntry)
                    {
                        alreadyInWaitlist = true;
                        break;
                    }
                }

                if (!alreadyInWaitlist)
                {
                    waitlistQueue.Enqueue(waitlistEntry);// Add to waitlist
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nClub is full (3/3 members).");
                    Console.WriteLine($"{studentName} has been added to the waitlist queue.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{studentName} is already in the waitlist queue.");
                    Console.ResetColor();
                }
            }
            else
            {
                // Enroll student using registry
                clubRegistry.EnrollStudent(currentAdminClub.GetClubName(), studentName);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSUCCESS: {studentName} has been enrolled in {currentAdminClub.GetClubName()}!");
                Console.ResetColor();
            }
        }
        public void DropStudentMenu() // case 3 admin menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Drop/Remove Student from My Club ---");
            Console.ResetColor();

            if (currentAdminClub.GetMembers().Count == 0) // no members to remove
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNo members to remove.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCurrent Members:");
            Console.ResetColor();

            int memberIndex = 1;
            foreach (var member in currentAdminClub.GetMembers())
            {
                string displayID = "S" + memberIndex.ToString("D3"); // S001, S002, S003...
                Console.WriteLine($"- {member.getFullName()} (ID: {displayID})");
                memberIndex++;
            }

            Console.Write("\nEnter Student Full Name to Remove: ");
            string studentName = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(studentName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid name.");
                Console.ResetColor();
                return;
            }

            // Find and remove student
            StudentMember studentToRemove = null;
            foreach (var member in currentAdminClub.GetMembers())
            {
                if (member.getFullName().ToLower() == studentName.ToLower()) // match found
                {
                    studentToRemove = member;
                    break;
                }
            }

            if (studentToRemove != null)
            {
                currentAdminClub.GetMembers().Remove(studentToRemove);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSUCCESS: {studentName} has been removed from {currentAdminClub.GetClubName()}.");
                Console.ResetColor();

                // Check if someone in queue can be admitted
                CheckAndAdmitFromQueue();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{studentName} not found in {currentAdminClub.GetClubName()}.");
                Console.ResetColor();
            }
        }

        public void ViewAllClubs() // case 4 admin menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- All Available Clubs ---");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n{0,-20} | {1,-15} | {2,-10} | {3,-10}", "Club Name", "Administrator", "Members", "Waitlist");
            Console.WriteLine("-------------------------------------------------------------");
            Console.ResetColor();

            foreach (var club in clubRegistry.GetClubs())
            {
                    
                string clubName = club.GetClubName();
                string adminName = club.GetAdministrator().AdminName();
                int membersCount = club.GetMembers().Count;

                // Count waitlist for this club
                int waitlistCount = 0;
                foreach (var entry in waitlistQueue)
                {
                    string[] parts = entry.Split('|');
                    if (parts.Length == 2 && parts[1] == clubName)
                    {
                        waitlistCount++;
                    }
                }
                if (membersCount < 3 && waitlistCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                }
                else if (membersCount >= 3 && waitlistCount > 0)
                        
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                    Console.WriteLine("{0,-20} | {1,-15} | {2,-10} | {3,-10}", clubName, adminName, membersCount + "/3", waitlistCount);
                    Console.WriteLine();
                Console.ResetColor();
            }
        }

       

        public void CheckWaitlistAndAdmit() // case 5 admin menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Check Waitlist & Admit Students ---");
            Console.ResetColor();

            // Count entries for current club in queue
            int myWaitlistCount = 0;
            foreach (var entry in waitlistQueue) // Check entries for this club
            {
                string[] parts = entry.Split('|');
                if (parts.Length == 2 && parts[1] == currentAdminClub.GetClubName()) // validates club name
                {
                    myWaitlistCount++;
                }
            }

            if (myWaitlistCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo students in waitlist queue.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\nAvailable Slots: {3 - currentAdminClub.GetMembers().Count}/3");
            Console.WriteLine($"Students in Waitlist Queue: {myWaitlistCount}");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nWaitlist Queue (in order):");
            Console.ResetColor();
            foreach (var entry in waitlistQueue)
            {
                string[] parts = entry.Split('|');
                if (parts.Length == 2 && parts[1] == currentAdminClub.GetClubName())
                {
                    Console.WriteLine($"- {parts[0]}");
                }
            }

            // Automatically admit from queue (FIFO - First In First Out)
            int admitted = 0;

            while (currentAdminClub.GetMembers().Count < 3 && waitlistQueue.Count > 0)
            {
                string entry = waitlistQueue.Peek(); // Look at first in queue
                string[] parts = entry.Split('|');

                if (parts.Length == 2 && parts[1] == currentAdminClub.GetClubName()) // validates club name
                {
                    string studentName = parts[0];

                    // Dequeue and enroll
                    waitlistQueue.Dequeue();

                    clubRegistry.EnrollStudent(currentAdminClub.GetClubName(), studentName);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{studentName} has been admitted from waitlist queue!");
                    Console.ResetColor();
                    admitted++;
                }
                else
                {
                    // Entry is for different club, stop processing
                    break;
                }
            }

            if (admitted > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nTotal admitted: {admitted}");
                Console.ResetColor();
            }

            // Show remaining in queue
            int remainingCount = 0;
            foreach (var entry in waitlistQueue)
            {
                string[] parts = entry.Split('|');
                if (parts.Length == 2 && parts[1] == currentAdminClub.GetClubName())
                {
                    remainingCount++;
                }
            }

            if (remainingCount > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nRemaining in waitlist queue: {remainingCount}");
                Console.ResetColor();
            }
        }
        public void CheckAndAdmitFromQueue() // case 5 admin menu helper
        {
            // Automatically admit from queue if space available
            while (currentAdminClub.GetMembers().Count < 3 && waitlistQueue.Count > 0)
            {
                string entry = waitlistQueue.Peek(); // Look at first entry
                string[] parts = entry.Split('|');

                if (parts.Length == 2 && parts[1] == currentAdminClub.GetClubName())
                {
                    string studentName = parts[0];

                    // Dequeue and enroll
                    waitlistQueue.Dequeue(); // Remove from queue
                    clubRegistry.EnrollStudent(currentAdminClub.GetClubName(), studentName);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nAuto-admitted from queue: {studentName}");
                    Console.ResetColor();
                }
                else
                {
                    // Entry is for different club, skip
                    break;
                }
            }
        }

        public void PostAnnouncementMenu() // case 6 admin menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Post Club Announcement ---");
            Console.ResetColor();

            Console.Write("\nEnter announcement message: ");
            string announcement = Console.ReadLine().Trim();

            if (announcement != null)
            {
                clubAnnouncements.Add($"{currentAdminClub.GetClubName()}:[Posted by {currentAdminClub.GetAdministrator().AdminName()}] {announcement}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nAnnouncement posted successfully for {currentAdminClub.GetClubName()}.");
                Console.WriteLine("All club members will receive this.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAnnouncement cannot be empty.");
                Console.ResetColor();
            }
        }

        // ===============================================
        //                  STUDENT FLOW
        // ===============================================

        public void StudentLoginAndMenu() // student login and menu flow
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Student Login ---");
            Console.ResetColor();

            Console.Write("\nEnter your full name to access your club membership: ");
            string studentName = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(studentName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid name. Returning to main menu...");
                Console.ResetColor();
                Thread.Sleep(1500);
                return;
            }

            // 1. Check if the student is a member of any club
            Clubs memberClub = null;
            foreach (var club in clubRegistry.GetClubs())
            {
                foreach (var member in club.GetMembers())
                {
                    if (member.getFullName().ToLower() == studentName.ToLower())
                    {
                        memberClub = club;
                        break;
                    }
                }
                if (memberClub != null)
                {
                    break;
                }
            }

            if (memberClub == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAccess Denied. {studentName} is not registered as a member of any club.");
                Console.WriteLine("Please enroll first or check your name spelling.");
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            // --- Successful Login ---
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nLoading Student Menu...");
            Console.ResetColor();
            Thread.Sleep(800);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome, {studentName} to your club {memberClub.GetClubName()}");
         
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to continue to your member menu...");

            Console.ReadKey();

            // 2. Proceed to the member menu
            StudentMenuLoop(studentName, memberClub);
        }

        public void StudentMenuLoop(string studentName, Clubs memberClub) // student menu loop
        {
            bool exit = false;
            while (!exit)
            {
                

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=====================================");
                Console.WriteLine($"      STUDENT MENU - {memberClub.GetClubName().ToUpper()}");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=====================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n1. View My Club Membership");
                Console.WriteLine("\n2. View Club Announcements");
                Console.WriteLine("\n3. Return to Main Menu");
                Console.ResetColor();
                Console.Write("\nEnter your choice: ");

                string choiceInput = Console.ReadLine();

                if (int.TryParse(choiceInput, out int choice))
                {
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            ViewMyMembership(studentName);
                            break;
                        case 2:
                            ViewAnnouncementsMenu(studentName);
                            break;
                        case 3:
                            exit = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nReturning to Main Menu...");
                            Console.ResetColor();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid choice. Please select a valid option (1-3).");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a number.");
                    Console.ResetColor();
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
      
      
        public void ViewMyMembership(string studentName) // case 1 student menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- My Club Membership ---");
            Console.ResetColor();

            bool foundMembership = false;
            Clubs enrolledClub = null;
            StudentMember studentMember = null;

            // Find student's club
            foreach (var club in clubRegistry.GetClubs())
            {
                foreach (var member in club.GetMembers())
                {
                    if (member.getFullName().ToLower() == studentName.ToLower()) // match found
                    {
                        enrolledClub = club;
                        studentMember = member;
                        foundMembership = true;
                        break;
                    }
                }
                if (foundMembership)
                {
                    break;
                }
            }

            

            if (foundMembership)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nYou are enrolled in a club!");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nClub Name: {enrolledClub.GetClubName()}");
                Console.WriteLine($"\nClub Administrator: {enrolledClub.GetAdministrator().AdminName()}");
                Console.WriteLine($"\nTotal Members: {enrolledClub.GetMembers().Count}/3");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou are not currently enrolled in any club.");
                Console.ResetColor();

                // Check if in waitlist
                bool inWaitlist = false;
                string waitlistClub = "";
                foreach (var entry in waitlistQueue)
                {
                    string[] parts = entry.Split('|');
                    if (parts.Length == 2 && parts[0].ToLower() == studentName.ToLower()) // validates name
                    {
                        inWaitlist = true;
                        waitlistClub = parts[1];
                        break;
                    }
                }

                if (inWaitlist)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nYou are in the waitlist for: {waitlistClub}");
                    Console.WriteLine("You will be automatically enrolled when a slot becomes available.");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("\nPlease contact an administrator to enroll in a club.");
                }
            }
        }

        public void ViewAnnouncementsMenu(string studentName) // case 2 student menu
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Club Announcements ---");
            Console.ResetColor();

            bool foundMembership = false;
            string clubName = "";

            // Find student's club
            foreach (var club in clubRegistry.GetClubs())
            {
                foreach (var member in club.GetMembers())
                {
                    if (member.getFullName().ToLower() == studentName.ToLower()) // match found
                    {
                        clubName = club.GetClubName();
                        foundMembership = true;
                        break;
                    }
                }
                if (foundMembership)
                {
                    break;
                }
            }

            if (foundMembership)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nAnnouncements for {clubName}:");
                Console.WriteLine("-------------------------------------");
                Console.ResetColor();

                bool hasAnnouncements = false;
                foreach (var announcement in clubAnnouncements)
                {
                    if (announcement.StartsWith(clubName + ":")) // Match club
                    {
                        string message = announcement.Substring(clubName.Length + 1); // Extract message
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\n{message}");
                        hasAnnouncements = true;
                        Console.ResetColor();
                    }
                }

                if (!hasAnnouncements)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo new announcements.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou are not enrolled in any club.");
                Console.WriteLine("Join a club to see announcements!");
                Console.ResetColor();
            }
        }
    }
}
