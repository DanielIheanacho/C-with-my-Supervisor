using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Period
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public bool IsBreakPeriod { get; set; }
    }
}
