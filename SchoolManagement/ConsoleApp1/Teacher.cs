using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Teacher : User
    {
        public string StaffId { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        
        public void Print()
        {
            Console.WriteLine("Id: " + this.Id);
            Console.WriteLine("StaffNumber: " + this.StaffId);
            Console.WriteLine("Name: " + this.FirstName + this.LastName);
            Console.WriteLine("Email: " + this.Email);
            Console.WriteLine("PhoneNumber: " + this.PhoneNumber);
            Console.WriteLine("Department: " + Department);
            Console.WriteLine("Designation: " + this.Designation);
        }
    }
}
