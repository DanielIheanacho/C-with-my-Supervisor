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
        case "0":
            return;
        case "1":
            var schoolInfo = InputSchoolDetail();
            var userInfo = InputUserInfo(true);
            await AuthenticationService.RegisterSchoolAndAdminAsync(schoolInfo, userInfo);
            break;

        case "2":
            Console.WriteLine("\n\nEmail: ");
            string email = ValidInput();
            Console.WriteLine("Password: ");
            string passWord = ValidInput();
            var user = await AuthenticationService.LoginUserAsync(email, passWord);
            if (user == null)
            {
                Console.WriteLine("Invalid email or password.");
                break;
            }

            Console.WriteLine($"Welcome, {user.FirstName}!");

            switch (user)
            {
                case Admin admin:
                    await AdminMenuAsync(admin);
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


async Task AdminMenuAsync(Admin admin)
{
    while (true)
    {
        Console.WriteLine("---Menu---\n");
        Console.WriteLine("1. Register New user");
        Console.WriteLine("2. View Admin(s)");
        Console.WriteLine("3. View Teahers(s)");
        Console.WriteLine("4. View Guardian(s)");
        Console.WriteLine("5. Remove User");
        Console.WriteLine("6. Update Details");
        Console.WriteLine("0. Exit\n\n");
        Console.Write("Select Option: ");

        switch (Console.ReadLine())
        {
            case "0":
                return; ;
            case "1":
                {
                    var userInfo = InputUserInfo(false);
                    User newUser = admin.RegisterNewUser(userInfo);
                    await UserService.SaveUserAsync(newUser);
                    break;
                }
            case "2":
                await PrintUsersInCategoryAsync(admin, Roles.Admin);
                break;
            case "3":
                await PrintUsersInCategoryAsync(admin, Roles.Teacher);
                break;
            case "4":
                await PrintUsersInCategoryAsync(admin, Roles.Guardian);
                break;
            case "5":
                await DeleteUserAsync(admin);
                break;
            case "6":
                await UpdateDetailsAsync(admin);
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
}

async Task<(int Count, List<UserInfo> List)> PrintUsersInCategoryAsync(Admin admin, Roles role)

{
    var users = await UserService.GetUsersInCategoryAsync(admin, role);

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

async Task UpdateDetailsAsync<T>(T user) where T : User, new()
{
    var userInfo = ExtractUserInfo(user);
    while (true)
    {
        Console.WriteLine("1. Firstname");
        Console.WriteLine("2. Lastame");
        Console.WriteLine("3. Email");
        Console.WriteLine("4. Password");
        Console.WriteLine("0. Exit");
        Console.WriteLine("Select Option: ");
        switch (Console.ReadLine())
        {
            case "1":
                userInfo.FirstName = ValidInput();
                await UserService.UpdateUserAsync(user, userInfo);
                break;
            case "2":
                userInfo.LastName = ValidInput();
                await UserService.UpdateUserAsync(user, userInfo);
                break;
            case "3":
                userInfo.Email = ValidInput();
                await UserService.UpdateUserAsync(user, userInfo);
                break;
            case "4":
                userInfo.PassWord = ValidInput();
                await UserService.UpdateUserAsync(user, userInfo);
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }

}

UserInfo ExtractUserInfo<T>(T user) where T : User, new()
{
    var userInfo = new UserInfo
    {
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        PassWord = user.PassWord,
    };

    return userInfo;
}

async Task DeleteUserAsync(Admin admin)
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
                await DeleteUserInCategoryAsync(admin, Roles.Admin);
                break;
            case "2":
                await DeleteUserInCategoryAsync(admin, Roles.Teacher);
                break;
            case "3":
                await DeleteUserInCategoryAsync(admin, Roles.Guardian);
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
}

async Task DeleteUserInCategoryAsync(Admin admin, Roles role)
{
    var result = await PrintUsersInCategoryAsync(admin, role);
    var userCount = result.Count;
    var user = result.List;
    Console.WriteLine("Select option:");
    if (int.TryParse(Console.ReadLine(), out int option) && option > 0 && option <= userCount)
    {
        int userId = user[option - 1].UserId;
        await UserService.RemoveUserAsync(userId);
    }
}

SchoolInfo InputSchoolDetail()
{
    SchoolInfo school = new();
    Console.WriteLine("SchooL Name: ");
    school.Name = ValidInput();
    return school;
}

UserInfo InputUserInfo(bool isFirstUser)
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
    if (!isFirstUser)
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