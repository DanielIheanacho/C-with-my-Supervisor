using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Admin : User
    {
        public string StaffId { get; set; }

        public static Admin Register()
        {
            School school = School.RegisterSchool();
            Console.WriteLine("New School registered.");

            Admin admin = new()
            {
                Id = userCount++,
                Role = Roles.Admin
            };

            Console.WriteLine("----Fill Form----");
            Console.WriteLine("FirstName: ");
            admin.FirstName = Console.ReadLine();

            Console.WriteLine("LastName: ");
            admin.LastName = Console.ReadLine();

            Console.WriteLine("Email: ");
            admin.Email = Console.ReadLine();

            Console.WriteLine("Password: ");
            admin.PassWord = Console.ReadLine();
            Console.WriteLine("New Admin registered for school " + admin.SchoolId);

            school.users.Add(admin);

            return admin;
        }

        public void RegisterTeacher()
        {
            Teacher teacher = new Teacher
            {
                Id = userCount++,
                Role = Roles.Teacher,
                SchoolId = this.SchoolId
            };

            Console.WriteLine("----Fill Form----");
            Console.WriteLine("FirstName: ");
            teacher.FirstName = Console.ReadLine();

            Console.WriteLine("LastName: ");
            teacher.LastName = Console.ReadLine();

            Console.WriteLine("Email: ");
            teacher.Email = Console.ReadLine();

            Console.WriteLine("Password: ");
            teacher.PassWord = Console.ReadLine();
            Console.WriteLine("New Teacher registered for school " + teacher.SchoolId);

        }
    }
}
