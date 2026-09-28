using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models
{
    public class Guardian : User
    {
        public List<Student> students = new();

            public void Print()
        {
            Console.WriteLine("User id: " + this.Id);
            Console.WriteLine("Name: " + this.LastName + " " + this.FirstName);
            Console.WriteLine("Email: " + this.Email);
            Console.WriteLine("Role: " + this.Role);
            Console.Write("Password: ******");
            foreach (Student student in students)
            {
                Console.Write(student.LastName + " " + student.FirstName + " ");
            }
        }
    }
}
