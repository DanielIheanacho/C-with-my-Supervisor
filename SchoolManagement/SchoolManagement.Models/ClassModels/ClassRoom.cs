namespace SchoolManagement.Models.ClassModels
{
    public class ClassRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int SchoolId { get; set; }

        public ClassRoom CreateClassRoom<T>(ClassRoomInfo classRoomInfo, T user) where T : User, new()
        {
            ClassRoom classRoom = new();
            classRoom.Name = classRoomInfo.Name;
            classRoom.Capacity = classRoomInfo.Capacity;
            classRoom.SchoolId = user.SchoolId;

            return classRoom;
        }
    }  
}