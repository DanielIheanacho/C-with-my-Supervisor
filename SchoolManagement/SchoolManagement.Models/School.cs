namespace SchoolManagement.Models
{

    public class School
    {
        public static List<School> schools = new();
        public int SchoolId { get; set; }
        public string Name { get; set; }

        public static School RegisterSchool()
        {
            School school = new();
            Console.WriteLine("SchooL Name: ");
            school.Name = ValidInput();
            schools.Add(school);
            return school;
        }

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

        //public List<Student> students = new();
        //public static List<Subject> subjects = new();
        //public static List<ClassRoom> classRooms = new();

        //public static void AddSubject(Subject subject)
        //{
        //    foreach (School school in schools)
        //    {
        //        if (school.SchoolId == subject.SchoolId)
        //        {
        //            School.subjects.Add(subject);
        //            return;
        //        }
        //    }
        //    Console.WriteLine("Subject is not Registered ubder any school.");
        //}

        //public static void AddStudent(Student student)
        //{
        //    foreach (School school in schools)
        //    {
        //        if (school.SchoolId == student.SchoolId)
        //        {
        //            school.students.Add(student);
        //            return;
        //        }
        //    }
        //    Console.WriteLine("Student is not Registered ubder any school.");
        //}

        //public static void AddClassRoom(ClassRoom classroom)
        //{
        //    foreach (School school in schools)
        //    {
        //        if (school.SchoolId == classroom.SchoolId)
        //        {
        //            School.classRooms.Add(classroom);
        //            return;
        //        }
        //    }
        //    Console.WriteLine("ClassRoom is not Registered under any school.");
        //}
    }
}
