namespace SchoolManagement.Models
{
    public class Teacher : User
    {
        public string TeacherId { get; set; }
        public string Department { get; set; }
        public Designation Designation { get; set; }
    }
}
