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

        public void Print()
        {
            Console.WriteLine("User id: " + this.Id);
            Console.WriteLine("Name: " + this.LastName + this.FirstName);
            Console.WriteLine("Email: " + this.Email);
            Console.WriteLine("Role: " + this.Role);
            Console.WriteLine("Password: " + this.PassWord);
        }

        public User()
        {

        }

        public static User RegisterUser()
        {

            User user = new();


            user.Id = userCount++;

            //user.Role = Roles.Default;

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
    }
}
