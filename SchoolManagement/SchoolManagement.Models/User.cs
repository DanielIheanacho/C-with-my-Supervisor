namespace SchoolManagement.Models
{
    public class User
    {
        public static int userCount = 0;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public string PassWord { get; set; }
        public int SchoolId { get; set; }


        public User()
        {

        }

        public static User RegisterUser()
        {
            User user = FillUserDetail();
            user.Role = SelectRole();
            //Console.WriteLine("Rgistered new User");
            //user.Print();
            return user;
        }

        public static void LoginUser()
        {
            Console.WriteLine("\nPlease Login");
            Console.WriteLine("\n\nEmail: ");
            string email = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            foreach(User user in School.users)
            {
                if (user.Email == email && user.PassWord == password)
                    Console.WriteLine("Welcome to the School Management app");
                else
                    Console.WriteLine("Invalid Input");
            }
        }

        // Method to login user

        private static User FillUserDetail()
        {
            User user = new();
            Console.WriteLine("----Fill Form----");
            Console.WriteLine("FirstName: ");
            user.FirstName = Console.ReadLine();

            Console.WriteLine("LastName: ");
            user.LastName = Console.ReadLine();

            Console.WriteLine("Email: ");
            user.Email = Console.ReadLine();

            Console.WriteLine("Password: ");
            user.PassWord = Console.ReadLine();

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
}
