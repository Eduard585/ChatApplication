using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class UserFilter
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime UpdDate { get; set; }
    }
}
