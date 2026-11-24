namespace SchoolManagement.Web.DTOs
{
    public record LoginRequest
    {
        public required string Email { get; set; }
        public required string PassWord { get; set; }
    }
}
