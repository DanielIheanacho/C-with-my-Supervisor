namespace SchoolManagement.Models
{
    public class Teacher : User
    {
        public string StaffId { get; set; }
        public string Department { get; set; }
        public Designation Designation { get; set; }

        public void Print()
        {
            Console.WriteLine("Id: " + this.Id);
            Console.WriteLine("StaffNumber: " + this.Id);
            Console.WriteLine("Name: " + this.FirstName + this.LastName);
            Console.WriteLine("Email: " + this.Email);
            Console.WriteLine("PhoneNumber: " + this.PhoneNumber);
            Console.WriteLine("Department: " + Department);
            Console.WriteLine("Designation: " + this.Designation);
        }
    }
}
