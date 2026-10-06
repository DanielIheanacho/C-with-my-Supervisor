using SchoolManagement.Models.EnumModels;

namespace SchoolManagement.Models.ClassModels
{
    public class Admin : User
    {
        public static Admin RegisterNewAdmin(School school, UserInfo userInfo)
        {
            Admin user = FillUserDetail<Admin>(userInfo);
            user.Role = Roles.Admin;
            user.SchoolId = school.SchoolId;
            return user;
        }

        public User RegisterNewUser(UserInfo userInfo)
        {
            switch (SelectRole(userInfo))
            {
                case Roles.Admin:
                    return RegisterAnyUser<Admin>(userInfo);
                case Roles.Teacher:
                    return RegisterAnyUser<Teacher>(userInfo);
                case Roles.Guardian:
                    return RegisterAnyUser<Guardian>(userInfo);
                default:
                    throw new Exception("Something went wrong.");
            }
        }

        public T RegisterAnyUser<T>(UserInfo userInfo) where T : User, new()
        {
            T user = FillUserDetail<T>(userInfo);
            user.SchoolId = this.SchoolId;
            user.Role = SelectRole(userInfo);
            Console.WriteLine(user.SchoolId);
            return user;
        }

        public static Roles SelectRole(UserInfo userInfo)
        {
            switch (userInfo.Role.ToLower())
            {
                case "admin":
                    return Roles.Admin;
                case "teacher":
                    return Roles.Teacher;
                case "guardian":
                    return Roles.Guardian;
                default:
                    throw new Exception("Something went wrong.");
            }
        }
    }
}