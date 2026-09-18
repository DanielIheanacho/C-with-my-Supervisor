using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    public class School
    {
        public static int schoolId = 0;
        static List<School> schools = new();
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> users =  new();
        //public List<Student> students = new();
        //public List<Subject> subjects = new();
        //public List<ClassRoom> classRooms = new();

        public static School RegisterSchool()
        {
            School school = new();
            Console.WriteLine("SchooL Name: ");
            school.Name = Console.ReadLine();
            school.Id = School.schoolId;
            schools.Add(school);
            return school;
        }

        public static void AssignUser(User user)
        {
            for (int i = 0; i < schools.Count; i++)
            {
                if (schools[i].Id != user.SchoolId && i < schools.Count - 1)
                {
                    continue;
                }
                else if (schools[i].Id == user.SchoolId)
                {
                    schools[i].users.Add(user);
                    return;
                }
                Console.WriteLine("User was assigned to a school not in database.");
            }
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
