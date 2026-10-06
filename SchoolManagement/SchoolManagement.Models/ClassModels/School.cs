namespace SchoolManagement.Models.ClassModels
{
    public class School
    {
        public static List<School> schools = new();
        public int SchoolId { get; set; }
        public string Name { get; set; }

        public static School RegisterSchool(SchoolInfo schoolInfo)
        {
            School school = new();
            school.Name = schoolInfo.Name;
            schools.Add(school);
            return school;
        }

    }
}
