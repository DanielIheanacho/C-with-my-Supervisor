using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Admin : User
    {
        public string StaffId { get; set; }
        School School { get; set; }

        public static Admin Register(School school)
        {
            //this.School = School.RegisterSchool();
            Console.WriteLine("New School registered.\n");

            Console.WriteLine("Register New Admin\n");
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
            Console.WriteLine("Register New Teacher\n");
            Teacher teacher = new Teacher
            {
                Id = userCount++,
                Role = Roles.Teacher,
                SchoolId = this.SchoolId
            };

            Console.WriteLine("----Fill Form ----");
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

        public void RegisterSubject()
        {
            Subject subject = new();
            Console.WriteLine("----Fill Form ----");
            Console.WriteLine("Name: ");
            subject.Name = Console.ReadLine();

            Console.WriteLine("Category: ");
            subject.Category = Console.ReadLine();

            Console.WriteLine("Is subject core: ");
            if(String.IsNullOrEmpty(Console.ReadLine()))
            {
                subject.IsCore = false;
            }
            else
            {
                subject.IsCore = true;
            }
            this.School.subjects.Add(subject);
            Console.WriteLine("New subject registered for school " );

        }
    }
}
