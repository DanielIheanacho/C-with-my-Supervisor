using SchoolManagementSystem.Services.SchoolManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

namespace SchoolManagementSystem.Service
{
    public class Service
    {
        public static void RegisterSchoolAndAdmin()
        {
            School school = School.RegisterSchool();
            User user = Admin.RegisterNewAdmin(school);
            using (var context = new AppDbContext())
            {
                context.Add(school);
                context.Add(user);
                context.SaveChanges();
            }
        }

        public static void LoginUser()
        {
            Console.WriteLine("\n\nEmail: ");
            string email = ValidInput().ToLower();

            Console.WriteLine("Password: ");
            string password = ValidInput();

            using var context = new AppDbContext();

            var user = context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email && u.PassWord == password);

            if (user == null)
            {
                Console.WriteLine("Invalid email or password.");
                return;
            }

            Console.WriteLine($"Welcome, {user.FirstName}!");

            switch (user)
            {
                case Admin admin:
                    AdminMenu(admin);
                    break;
                case Teacher teacher:
                    // teacher menu
                    break;
                case Guardian guardian:
                    // guardian menu
                    break;
            }
        }

        private static void AdminMenu(Admin admin)
        {
            while (true)
            {
                Console.WriteLine("---Menu---\n");
                Console.WriteLine("1. Register New user");
                Console.WriteLine("2. View Admin(s)");
                Console.WriteLine("3. View Teahers(s)");
                Console.WriteLine("4. View Guardian(s)");
                Console.WriteLine("0. Exit\n\n");
                Console.Write("Select Option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            User newUser = admin.RegisterNewUser();

                            using var context = new AppDbContext();
                            context.Add(newUser);
                            context.SaveChanges();
                            break;
                        }
                    case "2":
                        PrintUserNamesInCategory(admin, Roles.Admin);
                        break;
                    case "3":
                        PrintUserNamesInCategory(admin, Roles.Teacher);
                        break;
                    case "4":
                        PrintUserNamesInCategory(admin, Roles.Guardian);
                        break;
                    case "0":
                        return; ;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
        }

        private static void PrintUserNamesInCategory(Admin admin, Roles role)
        {
            using var context = new AppDbContext();

            var user = context.Users
                .AsNoTracking()
                .Where(u => u.SchoolId == admin.SchoolId && u.Role == role)
                .OrderBy(u => u.LastName)
                .Select(u => new { u.FirstName, u.LastName })
                .ToList();
            if (user.Count == 0)
            {
                Console.WriteLine("No users found.");
            }
            else
            {
                int i = 1;
                foreach (var u in user)
                {
                    Console.WriteLine($"{i++}. {u.FirstName} {u.LastName}");
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
                Console.WriteLine("Cannot be left Empty");
            }
        }
    }
}
