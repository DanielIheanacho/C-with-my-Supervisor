
namespace SchoolManagement.Models
{
    public class User
    {
        public static int userCount = 0;
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Genders? Gender { get; set; }
        public Roles? Role { get; set; }
        public string PassWord { get; set; }
        public int SchoolId { get; set; }

        public static T RegisterAnyUser<T>() where T : User, new()
        {
            T user = FillUserDetail<T>();
            user.SchoolId = SelectSchool();
            Console.WriteLine(user.SchoolId);
            return user;
        }

        internal static T FillUserDetail<T>() where T : User, new()
        {
            var user = new T();
            Console.WriteLine("----Fill Form----");
            Console.WriteLine("FirstName: ");
            user.FirstName = ValidInput();

            Console.WriteLine("LastName: ");
            user.LastName = ValidInput();

            Console.WriteLine("Email: ");
            user.Email = ValidInput().ToLower();

            Console.WriteLine("Password: ");
            user.PassWord = ValidInput();

            return user;

        }

        public static Roles SelectRole()
        {
            while (true)
            {
                Console.WriteLine("Insert an int value\n" +
                "Admin - 0\n" +
                "Teacher - 1\n" +
                "Guardian - 2\n\n" +
                "Insert users Role:");
                switch (Console.ReadLine())
                {
                    case "0":
                        return Roles.Admin;
                    case "1":
                        return Roles.Teacher;
                    case "2":
                        return Roles.Guardian;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

            }
        }

        private static int SelectSchool()
        {
            while (true)
            {
                int count = 0;
                foreach (School school in School.schools)
                {
                    Console.WriteLine("Select A School\n input must be int value\n\n");
                    Console.Write(count++ + " - " + school.Name);
                }
                if (int.TryParse(ValidInput(), out int option))
                {
                    if (option >= 0 && option <= School.schools.Count)
                    {
                        return School.schools[option].SchoolId;
                    }
                    else
                    {
                        Console.WriteLine("Pick option from List.");
                    }
                }
            }

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
            }
        }
    }
}
