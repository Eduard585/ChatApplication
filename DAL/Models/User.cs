using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }//TODO remove password
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime UpdDate { get; set; }
    }
}
