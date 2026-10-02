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

        public void RegisterUser()
        {
            Roles role = User.SelectRole();
            switch (role)
            {
                case Roles.Admin:
                    User.RegisterAnyUser<Admin>();
                    break;
                case Roles.Teacher:
                    User.RegisterAnyUser<Teacher>();
                    break;
                case Roles.Guardian:
                    User.RegisterAnyUser<Guardian>();
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