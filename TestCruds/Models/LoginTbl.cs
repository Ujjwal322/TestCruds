using System;
using System.Collections.Generic;

namespace TestCruds.Models
{
    public partial class LoginTbl
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
