namespace SchoolManagement.Models.ClassModels
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Genders? Gender { get; set; }
        public Roles? Role { get; set; }
        public string PassWord { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }

        internal static T FillUserDetail<T>(NewUser userDetail) where T : User, new()
        {
            var user = new T
            {
                FirstName = string.IsNullOrWhiteSpace(userDetail.FirstName) 
                ? throw new ArgumentException("First Name cannot be left empty.") 
                : userDetail.FirstName,

                LastName = string.IsNullOrWhiteSpace(userDetail.LastName)
                ? throw new ArgumentException("Last Name cannot be left empty.")
                : userDetail.LastName,

                Email = string.IsNullOrWhiteSpace(userDetail.Email)
                ? throw new ArgumentException("Email cannot be left empty.")
                : userDetail.Email.ToLower(),

                PassWord = string.IsNullOrWhiteSpace(userDetail.PassWord)
                ? throw new ArgumentException("Password cannot be left empty.")
                : userDetail.PassWord
            };

            return user;
        }
    }
}
