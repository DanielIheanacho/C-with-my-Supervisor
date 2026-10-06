namespace SchoolManagement.Models.ClassModels
{
    public class Student
    {
        public int Id { get; set; }

        public string AdmissionNumber { get; set; }
        public Genders Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int SchoolId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public int GuardianId { get; set; }

    }
}
