namespace SchoolManagement.Models
{
    public class Period
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsBreakPeriod { get; set; }
    }
}
