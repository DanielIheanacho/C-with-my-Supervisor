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
                    break;
            }
        }
    }
}