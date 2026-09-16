using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Subject
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public ClassRooms ClassRoom { get; set; }
        public bool IsCore { get; set; }

        public void Print() 
        {
            Console.WriteLine("ID: " + this.Id);
            Console.WriteLine("Name: " + this.Name);
            Console.WriteLine("Category: " + this.Category);
            Console.WriteLine("ClassRoom: " + this.ClassRoom);
            Console.WriteLine("IsCore " + this.IsCore);
        }
    }
}
