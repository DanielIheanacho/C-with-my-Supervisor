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
            Console.WriteLine("Name: " + this.LastName + " " + this.FirstName);
            Console.WriteLine("Email: " + this.Email);
            Console.WriteLine("Role: " + this.Role);
            Console.Write("Password: ******");
        }

        public User()
        {

        }

        public static User RegisterUser()
        {

            User user = new();


            user.Id = userCount++;

            Console.WriteLine("----Fill Form----");
            Console.WriteLine("FirstName: ");
            user.FirstName = Console.ReadLine();

            Console.WriteLine("LastName: ");
            user.LastName = Console.ReadLine();

            Console.WriteLine("Email: ");
            user.Email = Console.ReadLine();

            Console.WriteLine("Password: ");
            user.PassWord = Console.ReadLine();

            Console.WriteLine("Insert an I=int value"+
                "Admin - 0\n" +
                "Teacher - 1\n" +
                "Guardian - 2\n\n" +
                "Insert users Role:" 
                );
            if(int.TryParse(Console.ReadLine(), out int input))
            {
                user.Role = (Roles)input;
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }

            user.Print();

            return user;
        }
    }
}
