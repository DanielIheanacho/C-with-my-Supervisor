using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models
{
    public class Admin : User
    {
        public static Admin RegisterNewAdmin(School school)
        {
            Admin user = FillUserDetail<Admin>();
            user.Role = Roles.Admin;
            user.SchoolId = school.SchoolId;
            return user;
        }

            public User RegisterNewUser() 
        {
            Roles role = SelectRole();
            switch (role)
            {
                case Roles.Admin:
                    return RegisterAnyUser<Admin>(role);
                case Roles.Teacher:
                    return RegisterAnyUser<Teacher>(role);
                case Roles.Guardian:
                    return RegisterAnyUser<Guardian>(role);
                default:
                    throw new ArgumentOutOfRangeException(nameof(role));
            }
        }

        public T RegisterAnyUser<T>(Roles role) where T : User, new()
        {
            T user = FillUserDetail<T>();
            user.SchoolId = this.SchoolId;
            user.Role = role;
            Console.WriteLine(user.SchoolId);
            return user;
        }

        public static Roles SelectRole()
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
    }
}