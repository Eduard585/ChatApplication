using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.UserManagement
{
    public class SaveUserResult
    {
        public bool Success { get; set; }
        public bool IsNew { get; set; }
        public long UserId { get; set; }
        public string Error { get; set; }
    }
}
