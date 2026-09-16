using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ConsoleApp1
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public string Password { get; set; }
    }
}
