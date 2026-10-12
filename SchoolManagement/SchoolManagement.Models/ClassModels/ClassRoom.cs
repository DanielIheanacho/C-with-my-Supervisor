namespace SchoolManagement.Models.ClassModels
{
    public class ClassRoom
    {
        public int ClassRoomId { get;}
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int SchoolId { get; set; }
    }  
}