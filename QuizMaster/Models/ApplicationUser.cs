using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Models
{
    public class ApplicationUser 
    {
        public string Name { get; set; }
        public string RegNumber
        {
            get { return DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace("AM", "00").Replace("PM", "1").Replace(" ", ""); }
            set { }

        }
        public int Score { get; set; }


    }
}
