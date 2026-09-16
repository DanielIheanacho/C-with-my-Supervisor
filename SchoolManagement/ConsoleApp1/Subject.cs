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
        public ClassRoom ClassRoom { get; set; }
        public bool IsCore { get; set; }


    }
}
