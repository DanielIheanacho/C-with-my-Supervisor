namespace SchoolManagement.Models.ClassModels
{
    public class Teacher : User
    {
        public string? Department { get; set; }
        public Designation? Designation { get; set; }
    }
}
