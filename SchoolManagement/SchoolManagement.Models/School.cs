namespace SchoolManagement.Models
{

    public class School
    {
        public static int schoolId = 0;
        public static List<School> schools = new();
        public int Id { get; set; }
        public string Name { get; set; }
        public static List<User> users = new();
        public static List<Student> students = new();
        public static List<Subject> subjects = new();
        public static List<ClassRoom> classRooms = new();

        public static School RegisterSchool()
        {
            School school = new();
            Console.WriteLine("SchooL Name: ");
            school.Name = ValidInput();
            school.Id = School.schoolId++;
            schools.Add(school);
            return school;
        }

        public static void AssignUser(User user)
        {
            foreach(School school in schools)
            {
                if(school.Id == user.SchoolId)
                {
                    School.users.Add(user);
                    return;
                }
            }
            Console.WriteLine("User is Registered ubder any school.");
        }

        public static void AddSubject(Subject subject)
        {
            foreach (School school in schools)
            {
                if (school.Id == subject.SchoolId)
                {
                    School.subjects.Add(subject);
                    return;
                }
            }
            Console.WriteLine("Subject is not Registered ubder any school.");
        }

        public static void AddStudent(Student student)
        {
            foreach (School school in schools)
            {
                if (school.Id == student.SchoolId)
                {
                    School.students.Add(student);
                    return;
                }
            }
            Console.WriteLine("Student is not Registered ubder any school.");
        }

        public static void AddClassRoom(ClassRoom classroom)
        {
            foreach (School school in schools)
            {
                if (school.Id == classroom.SchoolId)
                {
                    School.classRooms.Add(classroom);
                    return;
                }
            }
            Console.WriteLine("ClassRoom is not Registered ubder any school.");
        }

        //Ensures input is valid
        private static string ValidInput()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                Console.WriteLine("Input Can Not be Empty");
            }
        }

        public void PrintUsers()
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users.Count != 0)
                {
                    Console.WriteLine(users[i].FirstName + " " + users[i].LastName);
                    Console.WriteLine(users[i].SchoolId);
                }
                else
                {
                    Console.WriteLine("No User");
                }
            }
        }
    }
}
