using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ConsoleApp1
{
    public class User
    {
        public int iD { get; set; }
        public string firstName {  get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public Roles Role { get; set; }
        public string password { get; set; }
    }
}
