
using SchoolManagementSystem.Service;

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
            Service.RegisterSchoolAndAdmin();
            break;

        case "2":
            Service.LoginUser();
            break;

        default:
            Console.WriteLine("Invalid Option");
            break;
    }
}

