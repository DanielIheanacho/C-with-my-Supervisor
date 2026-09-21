using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    public class School
    {
        public static int schoolId = 0;
        public static List<School> schools = [];
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Admin> users = new();
        //public List<Student> students = new();
        public List<Subject> subjects = new();
        //public List<ClassRoom> classRooms = new();

        public static School RegisterSchool()
        {
            School school = new();
            Console.WriteLine("SchooL Name: ");
            school.Name = Console.ReadLine();
            school.Id = School.schoolId++;
            Admin admin = Admin.Register(school);
            school.AdduserToSchool(admin);
            schools.Add(school);
            return school;
        }


        public void AdduserToSchool(User user)
        {
            user.SchoolId = this.Id;
        }
        public void PrintUsers()
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users.Count != 0)
                {
                    Console.WriteLine(users[i].FirstName +  " " +users[i].LastName);
                }
                else
                {
                    Console.WriteLine("No User");
                }
            }
        }
    }
}
