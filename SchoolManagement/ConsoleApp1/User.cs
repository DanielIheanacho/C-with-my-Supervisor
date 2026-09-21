using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ConsoleApp1
{
    public class User
    {
        public static int userCount = 0;
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public string PassWord { get; set; }
        public int SchoolId { get; set; }
        public string PhoneNumber { get; set; }

        public void Print()
        {
            Console.WriteLine("User id: " + this.Id);
            Console.WriteLine("Name: " + this.LastName + this.FirstName);
            Console.WriteLine("Email: " + this.Email);
            Console.WriteLine("Role: " + this.Role);
            Console.WriteLine("Password: " + this.PassWord);
        }

        
    }
}
