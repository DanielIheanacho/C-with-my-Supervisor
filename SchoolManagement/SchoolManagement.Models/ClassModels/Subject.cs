namespace SchoolManagement.Models.ClassModels
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public int SchoolId { get; set; }
        public bool IsCore { get; set; }
    }
}
