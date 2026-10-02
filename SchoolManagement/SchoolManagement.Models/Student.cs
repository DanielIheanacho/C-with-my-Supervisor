namespace SchoolManagement.Models
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
        public List<Guardian> Guardians { get; set; }

        public void Print()
        {
            Console.WriteLine("User id: " + this.Id);
            Console.WriteLine("Name: " + this.FirstName + this.MiddleName + this.LastName);
            Console.WriteLine("DOB: " + this.DateOfBirth);
            Console.WriteLine("ClassRoom: " + this.ClassRoom);
            Console.WriteLine("Gender: " + this.Gender);
            foreach(Guardian user in Guardians)
            {
                Console.Write(user + " "); 
            }
        }
    }
}
