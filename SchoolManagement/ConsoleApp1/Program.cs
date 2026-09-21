
using SchoolManagement.Models;

User user1 = new User();

user1.Id = 1;
user1.FirstName = "Daniel";
user1.LastName = "Iheanacho";
user1.Email = "dihanacho@gmail.com";
user1.Role = Roles.Guardian;
user1.PassWord = "********";

user1.Print();
Console.WriteLine();

ClassRoom ss1 = new ClassRoom();
ss1.Id = 10;
ss1.Name = "Childre of God";
ss1.Capacity = 20;

ss1.Print();
Console.WriteLine();

Student student1 = new Student();

student1.Id = 2;
student1.AdmissionNumber = "STU001";
student1.Gender = Genders.Male;
student1.FirstName = "Gidion";
student1.LastName = "Edoghotu";
student1.MiddleName = "Azibaobuom";
student1.DateOfBirth = DateTime.Now ;
student1.ClassRoom = ss1;

student1.Print();
Console.WriteLine();

Subject subject1 = new Subject();

subject1.Id = 7;
subject1.Name = "Mathematics";
subject1.Category = "Science";
subject1.ClassRoom = ss1;
subject1.IsCore = true;

subject1.Print();
Console.WriteLine();


Teacher teacher1 = new Teacher();

teacher1.Id = 8;
teacher1.StaffNumber = "STAFF001";
teacher1.Gender = Genders.Female;
teacher1.FirstName = "James";
teacher1.LastName = "Clear";
teacher1.Email = "JamesClear@gmail.com";
teacher1.PhoneNumber = "09036530590";
teacher1.Department = "Science";
teacher1.Designation = ss1;

teacher1.Print();
Console.WriteLine();