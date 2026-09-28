
namespace SchoolManagement.Models
{
    public class User
    {
        public static int userCount = 0;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Genders Gender { get; set; }
        public Roles Role { get; set; }
        public string PassWord { get; set; }
        public int SchoolId { get; set; }


        //public User()
        //{
        //}

        public static User RegisterNewAdmin()
        {
            School school = School.RegisterSchool();
            User user = FillUserDetail();
            user.Role = Roles.Admin;
            user.SchoolId = school.Id;
            //Console.WriteLine("Rgistered new User");
            //user.Print();
            return user;
        }

        //Craete a user
        public static User RegisterUser()
        {
            User user = FillUserDetail();
            user.Role = SelectRole();
            user.SchoolId = SelectSchool();
            Console.WriteLine(user.SchoolId);
            //User.AssignUserToSchool(user);
            //Console.WriteLine("Rgistered new User");
            //user.Print();
            return user;
        }

        public static void LoginUser()
        {
            Console.WriteLine("\nPlease Login");
            Console.WriteLine("\n\nEmail: ");
            string email = ValidInput().ToLower();
            Console.WriteLine("Password: ");
            string password = ValidInput();

            foreach(User user in School.users)
            {
                if (user.Email == email && user.PassWord == password)
                {
                    Console.WriteLine("Welcome to the School Management app");
                    return;
                }
            }
            Console.WriteLine("Invalid Email or Password");
        }


        // Method to Fill user data for new users
        private static User FillUserDetail()
        {
            User user = new();
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

        private static Roles SelectRole()
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
                        return School.schools[option].Id;
                    }
                    else
                    {
                        Console.WriteLine("Pick option from List.");
                    }
                }
            }
            
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
            }
        }

        //public void Print()
        //{
        //    Console.WriteLine("User id: " + this.Id);
        //    Console.WriteLine("Name: " + this.LastName + " " + this.FirstName);
        //    Console.WriteLine("Email: " + this.Email);
        //    Console.WriteLine("Role: " + this.Role);
        //    Console.Write("Password: ******");
        //}

    }
}
