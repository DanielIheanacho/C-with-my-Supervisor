namespace SchoolManagement.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string StaffNumber { get; set; }
        public Genders Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public ClassRoom Designation { get; set; }

        public void Print()
        {
            Console.WriteLine("Id: " + this.Id);
            Console.WriteLine("StaffNumber: " + this.StaffNumber);
            Console.WriteLine("Name: " + this.FirstName + this.LastName);
            Console.WriteLine("Email: " + this.Email);
            Console.WriteLine("PhoneNumber: " + this.PhoneNumber);
            Console.WriteLine("Department: " + Department);
            Console.WriteLine("Designation: " + this.Designation);
        }
    }
}
