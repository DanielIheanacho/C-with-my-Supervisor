namespace SchoolManagement.Models.ClassModels
{
    public class ClassRoom
    {
        public int ClassRoomId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int SchoolId { get; set; }
    }  
}