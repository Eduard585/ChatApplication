using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.ViewModels
{
    public class RestRoom
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsActive { get; set; }
        
        public List<long> Users { get; set; }
    }
}
