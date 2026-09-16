using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Student
    {
        public int Id { get; set; }
        public string AdmissionNumber { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurnName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ClassRoom Classroom { get; set; }    
    }
}
