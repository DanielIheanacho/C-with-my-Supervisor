using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Period
    {
        private int Id { get; set; }
        public string name { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public bool isBreakPeriod { get; set; }
    }
}
