namespace SchoolManagement.Models.ClassModels
{
    public class SubjectInfo
    {
        public string Name { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public int SchoolId { get; set; }
        public bool IsCore { get; set; }
    }
}
