﻿using System;
using System.Collections.Generic;

namespace TestCruds.Models
{
    public partial class Registration
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
