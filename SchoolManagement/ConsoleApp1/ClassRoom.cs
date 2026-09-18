using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class ClassRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public void Print()
        {
            Console.WriteLine("ClassRoom ID: " + this.Id);
            Console.WriteLine("Name: " + this.Name);
            Console.WriteLine("Capacity: " + this.Capacity);
        }
    }
}
