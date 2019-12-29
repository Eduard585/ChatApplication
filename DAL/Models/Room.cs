using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Room
    {
        public long Id { get; set; }
        public string Name { get; set; }     
        public DateTime? CreateDate { get; set; }
        public bool IsActive { get; set; }        
    }
}
