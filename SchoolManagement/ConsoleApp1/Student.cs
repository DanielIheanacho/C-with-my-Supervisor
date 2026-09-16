using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Student
    {
        public int Id { get; set; }
        public string AdmissionNumber { get; set; }
        public Genders Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ClassRooms ClassRoom { get; set; }

        public void Print()
        {
            Console.WriteLine("User id: " + this.Id);
            Console.WriteLine("Name: " +  this.FirstName + this.MiddleName + this.LastName );
            Console.WriteLine("DOB: " + this.DateOfBirth);
            Console.WriteLine("ClassRoom: " + this.ClassRoom);
            Console.WriteLine("Gender: " + this.Gender);
        }
    }
}
