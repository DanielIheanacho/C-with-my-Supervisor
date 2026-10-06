namespace SchoolManagement.Models.ClassModels
{
    public class User
    {
        public static int userCount = 0;
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Genders? Gender { get; set; }
        public Roles Role { get; set; }
        public string PassWord { get; set; }
        public int SchoolId { get; set; }

        internal static T FillUserDetail<T>(UserInfo userDetail) where T : User, new()
        {
            var user = new T
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                Email = userDetail.Email.ToLower(),
                PassWord = userDetail.PassWord
            };

            return user;
        }
    }
}
