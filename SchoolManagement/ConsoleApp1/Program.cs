
using ConsoleApp1.SchoolManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

Console.WriteLine("Welcome to our school Management Application");

while (true)
{
    Console.WriteLine("--------Menu--------\n");
    Console.WriteLine("1. Resgister New School");
    Console.WriteLine("2. Login");
    Console.WriteLine("0. Exit");
    Console.WriteLine("Select Opion: ");

    switch (Console.ReadLine())
    {
        case "0":
            return;

        case "1":
            RegisterSchoolAndAdmin();
            break;

        case "2":
            LoginUser();
            break;

        default:
            Console.WriteLine("Invalid Option");
            break;
    }
}

void RegisterSchoolAndAdmin()
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

void LoginUser()
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
            // admin menu
            break;
        case Teacher teacher:
            // teacher menu
            break;
        case Guardian guardian:
            // guardian menu
            break;
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