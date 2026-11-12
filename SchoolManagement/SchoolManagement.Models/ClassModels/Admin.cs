namespace SchoolManagement.Models.ClassModels
{
    public class Admin : User
    {
        public static Admin RegisterFirstAdmin(int schoolId, NewUser userInfo)
        {
            Admin user = FillUserDetail<Admin>(userInfo);
            user.Role = Roles.Admin;
            user.SchoolId = schoolId;
            return user;
        }

        private T  RegisterAnyUser<T>(NewUser userInfo, string role) where T : User, new()
        {
            T user = FillUserDetail<T>(userInfo);
            user.SchoolId = this.SchoolId;
            user.Role = SelectRole(role);
            return user;
        }

        private static Roles SelectRole(string role)
        {
            switch (role)
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

        public User RegisterNewUser(NewUser userInfo, string role)
        {
            switch (SelectRole(role))
            {
                case Roles.Admin:
                    return RegisterAnyUser<Admin>(userInfo, role);
                case Roles.Teacher:
                    return RegisterAnyUser<Teacher>(userInfo, role);
                case Roles.Guardian:
                    return RegisterAnyUser<Guardian>(userInfo, role);
                default:
                    throw new Exception("Something went wrong.");
            }
        }
    }
}