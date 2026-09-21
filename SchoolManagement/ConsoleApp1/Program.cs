
using SchoolManagement.Models;

Register().PrintUsers();

static School Register()
{   
    User user = User.RegisterUser();
    School school = School.RegisterSchool();
    school.users.Add(user);
    return school;
}

//ClassRoom ss1 = new ClassRoom();
//ss1.Id = 10;
//ss1.Name = "Childre of God";
//ss1.Capacity = 20;

//ss1.Print();
//Console.WriteLine();

//Student student1 = new Student();

//student1.Id = 2;
//student1.AdmissionNumber = "STU001";
//student1.Gender = Genders.Male;
//student1.FirstName = "Gidion";
//student1.LastName = "Edoghotu";
//student1.MiddleName = "Azibaobuom";
//student1.DateOfBirth = DateTime.Now ;
//student1.ClassRoom = ss1;

//student1.Print();
//Console.WriteLine();

//Subject subject1 = new Subject();

//subject1.Id = 7;
//subject1.Name = "Mathematics";
//subject1.Category = "Science";
//subject1.ClassRoom = ss1;
//subject1.IsCore = true;

//subject1.Print();
//Console.WriteLine();

//School school1 = new School();
//School.Add(school1);

//school1.Id = 1;
//school1.Name = "pletoria";

//User user1 = new();
//User user2 = new();
//User user3 = new();

//user1.Id = 1;
//user1.FirstName = "Daniel";
//user1.LastName = "Iheanacho";
//user1.Email = "dihanacho@gmail.com";
//user1.Role = Roles.Guardian;
//user1.PassWord = "********";
//user1.SchoolId =  1;

//user2.Id = 1;
//user2.FirstName = "Samuel";
//user2.LastName = "Iheanacho";
//user2.Email = "iheanachoSamuel@gmail.com";
//user2.Role = Roles.Guardian;
//user2.PassWord = "********";
//user2.SchoolId = 1;

//user3.Id = 1;
//user3.FirstName = "Joseph";
//user3.LastName = "Iheanacho";
//user3.Email = "jojo@gmail.com";
//user3.Role = Roles.Guardian;
//user3.PassWord = "********";
//user3.SchoolId = 1;

//School.AssignUser(user1);
//School.AssignUser(user2);
//School.AssignUser(user3);

//