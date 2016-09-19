using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookWebApp.Models
{
    public class BasicInfo
    {
        public int Id { get; set; }

        public string ProfilePhotoUrl { get; set; }

        public string CoverPhotoUrl { get; set; }

        public string AreaOfInterest { get; set; }

        public string Location { get; set; }

        public string Experience { get; set; }

        public string Education { get; set; }
       
        public int UserId { get; set; }
    }
}