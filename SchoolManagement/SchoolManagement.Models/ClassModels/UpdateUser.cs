namespace SchoolManagement.Models.ClassModels
{
    public record UpdateUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PassWord { get; set; }
        public string? Role { get; set; }
    }
}
