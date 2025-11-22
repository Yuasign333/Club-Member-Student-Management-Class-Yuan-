using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_StudentClubManager__Midterms_
{
    /// <summary>
    ///  handles the data related to club administrators
    /// </summary>
    public class ClubAdmin
    {
        // Fields
        public string adminID;
        public string adminName;

        public ClubAdmin(string adminID, string adminName)
        {
          this.adminID = adminID;
          this.adminName = adminName;
        }
        public string GetadminID()
        {
            return adminID;
        }

        public string AdminName()
        {
            return adminName;
        }
    }
}