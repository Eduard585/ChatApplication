using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.ViewModels
{
    public class RestToken
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
