namespace SchoolManagement.Models.ClassModels
{
    public class School
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        public static School RegisterSchool(SchoolInfo schoolInfo)
        {
            School school = new();
            school.Name = schoolInfo.Name;
            return school;
        }

    }
}
