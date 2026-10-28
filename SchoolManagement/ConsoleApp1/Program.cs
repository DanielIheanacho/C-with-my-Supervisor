using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Interfaces;
using SchoolManagementSystem.Service.Repositories;
using SchoolManagementSystem.Service.Service;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Data Source=localhost\\SQLEXPRESS;Initial Catalog=SchoolManagementSystem;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Command Timeout=0"));

// Register your dependencies
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthenticationService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var userService = scope.ServiceProvider
    .GetRequiredService<UserService>();
var authenticationService = scope.ServiceProvider
    .GetRequiredService<AuthenticationService>();



Console.WriteLine("UserService is ready for testing.");

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
            await authenticationService.RegisterSchoolAndAdminAsync(schoolInfo, userInfo);
            break;

        //case "2":
        //    Console.WriteLine("\n\nEmail: ");
        //    string email = ValidInput();
        //    Console.WriteLine("Password: ");
        //    string passWord = ValidInput();
        //    var user = await authenticationService.LoginUserAsync(email, passWord);
        //    if (user == null)
        //    {
        //        Console.WriteLine("Invalid email or password.");
        //        break;
        //    }

        //    Console.WriteLine($"Welcome, {user.FirstName}!");

        //    switch (user)
        //    {
        //        case Admin admin:
        //            await AdminMenuAsync(admin);
        //            break;
        //        case Teacher teacher:
        //            // teacher menu
        //            break;
        //        case Guardian guardian:
        //            // guardian menu
        //            break;
        //    }
        //    break;

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

                    break;
                }
            //case "2":
            //    await PrintUsersInCategoryAsync(admin, Roles.Admin);
            //    break;
            //case "3":
            //    await PrintUsersInCategoryAsync(admin, Roles.Teacher);
            //    break;
            //case "4":
            //    await PrintUsersInCategoryAsync(admin, Roles.Guardian);
            //    break;
            case "5":
                await DeleteUserAsync(admin);
                break;
            //case "6":
            //    await UpdateDetailsAsync(admin);
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
}

async Task<(int Count, List<NameAndIdDto> List)> PrintUsersInCategoryAsync(Admin admin, Roles role)

{
    var users = await userService.GetUsersInCategoryAsync(admin, role);

    if (users.Count == 0)
    {
        Console.WriteLine("No users found.");
    }
    else
    {
        int i = 1;
        foreach (var u in users)
        {
            Console.WriteLine($"{i++}. {u.Name}");
        }
    }
    return (users.Count, users);
}

async Task UpdateDetailsAsync<T>(T user) where T : User, new()
{
    var userInfo = ExtractUserInfo(user);
    var Id = user.UserId;
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
                await userService.UpdateUserAsync(Id, userInfo);
                break;
            case "2":
                userInfo.LastName = ValidInput();
                await userService.UpdateUserAsync(Id, userInfo);
                break;
            case "3":
                userInfo.Email = ValidInput();
                await userService.UpdateUserAsync(Id, userInfo);
                break;
            case "4":
                userInfo.PassWord = ValidInput();
                await userService.UpdateUserAsync(Id, userInfo);
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
    var users = result.List;
    Console.WriteLine("Select option:");
    if (int.TryParse(Console.ReadLine(), out int option) && option > 0 && option <= userCount)
    {
        int userId = users[option - 1].Id;
        await userService.DeleteUserAsync(userId);
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