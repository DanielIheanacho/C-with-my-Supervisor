namespace SchoolManagement.Models.ClassModels
{
    public record NewUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PassWord { get; set; }
        public required string Role { get; set; }
    }
}
