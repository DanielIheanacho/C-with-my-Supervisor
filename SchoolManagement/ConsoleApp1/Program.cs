using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service;

while (true)
{
    Console.WriteLine("---Menu---\n");
    Console.WriteLine("1. Register new School");
    Console.WriteLine("2. Login");
    Console.WriteLine("0. Exit\n\n");
    Console.Write("Select Option: ");

    switch (Console.ReadLine())
    {
        case "1":
            var schoolInfo = GetSchoolDetail();
            var userInfo = GetUserInfo(true);
            AuthenticationService.RegisterSchoolAndAdmin(schoolInfo, userInfo);
            break;

        case "2":
            Console.WriteLine("\n\nEmail: ");
            string email = ValidInput();
            Console.WriteLine("Password: ");
            string passWord = ValidInput();
            var user = AuthenticationService.LoginUser(email, passWord);
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
        break;

    }
}


void AdminMenu(Admin admin)
{
    while (true)
    {
        Console.WriteLine("---Menu---\n");
        Console.WriteLine("1. Register New user");
        Console.WriteLine("2. View Admin(s)");
        Console.WriteLine("3. View Teahers(s)");
        Console.WriteLine("4. View Guardian(s)");
        Console.WriteLine("5. Remove User");
        Console.WriteLine("0. Exit\n\n");
        Console.Write("Select Option: ");

        switch (Console.ReadLine())
        {
            case "1":
                {
                    var userInfo = GetUserInfo(false);
                    User newUser = admin.RegisterNewUser(userInfo);
                    AdminService.SaveUser(newUser);
                    break;
                }
            case "2":
                PrintUsersInCategory(admin, Roles.Admin);
                break;
            case "3":
                PrintUsersInCategory(admin, Roles.Teacher);
                break;
            case "4":
                PrintUsersInCategory(admin, Roles.Guardian);
                break;
            case "5":
                DeleteUser(admin);
                break;
            case "0":
                return; ;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
}

void AdminMenu(Admin admin)
{
    while (true)
    {
        Console.WriteLine("---Menu---\n");
        Console.WriteLine("1. Register New user");
        Console.WriteLine("2. View Admin(s)");
        Console.WriteLine("3. View Teahers(s)");
        Console.WriteLine("4. View Guardian(s)");
        Console.WriteLine("5. Remove User");
        Console.WriteLine("0. Exit\n\n");
        Console.Write("Select Option: ");

        switch (Console.ReadLine())
        {
            case "1":
                {
                    var userInfo = GetUserInfo(false);
                    User newUser = admin.RegisterNewUser(userInfo);
                    AdminService.SaveUser(newUser);
                    break;
                }
            case "2":
                PrintUsersInCategory(admin, Roles.Admin);
                break;
            case "3":
                PrintUsersInCategory(admin, Roles.Teacher);
                break;
            case "4":
                PrintUsersInCategory(admin, Roles.Guardian);
                break;
            case "5":
                DeleteUser(admin);
                break;
            case "0":
                return; ;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
}

    (int Count, List<UserInfo> List) PrintUsersInCategory(Admin admin, Roles role)

    {
        var users = AdminService.GetUsersInCategory(admin, role);

        if (users.Count == 0)
        {
            Console.WriteLine("No users found.");
        }
        else
        {
            int i = 1;
            foreach (var u in users.List)
            {
                Console.WriteLine($"{i++}. {u.FirstName} {u.LastName}");
            }
        }
        return (users.Count, users.List);
    }

    void DeleteUser(Admin admin)
    {
        while (true)
        {
            Console.WriteLine("What category of users would you like to delete?");
            Console.WriteLine("1. Admin(s)");
            Console.WriteLine("2. Teahers(s)");
            Console.WriteLine("3. Guardian(s)");
            Console.WriteLine("0. Exit\n\n");
            Console.Write("Select Option: ");

            switch (Console.ReadLine())
            {
                case "0":
                    return;
                case "1":
                    var result = PrintUsersInCategory(admin, Roles.Admin);
                    var userCount = result.Count;
                    var user = result.List;
                    Console.WriteLine("Select option:");
                    if (int.TryParse(Console.ReadLine(), out int option) && option > 0 && option <= userCount)
                    {
                        int userId = user[option - 1].UserId;
                        AdminService.RemoveUser(userId);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }
    }

    SchoolInfo GetSchoolDetail()
    {
        SchoolInfo school = new();
        Console.WriteLine("SchooL Name: ");
        school.Name = ValidInput();
        return school;
    }

    UserInfo GetUserInfo(bool isAdmin)
    {
        UserInfo user = new();

        Console.WriteLine("----Fill User Details----");
        Console.WriteLine("FirstName: ");
        user.FirstName = ValidInput();

        Console.WriteLine("LastName: ");
        user.LastName = ValidInput();

        Console.WriteLine("Email: ");
        user.Email = ValidInput().ToLower();

        Console.WriteLine("Password: ");
        user.PassWord = ValidInput();
        if (!isAdmin)
        {
            Console.WriteLine("Role: ");
            user.Role = SelectRole();
        }
        else
        {
            user.Role = "admin";
        }

        return user;
    }

    string SelectRole()
    {
        while (true)
        {
            Console.WriteLine("Insert an int value\n" +
            "Admin - 1\n" +
            "Teacher - 2\n" +
            "Guardian - 3\n" +
            "Exit - 0\n\n" +
            "Insert users Role:");
            switch (Console.ReadLine())
            {
                case "1":
                    return "admin";
                case "2":
                    return "teacher";
                case "3":
                    return "guardian";
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }
    }

    static string ValidInput()
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