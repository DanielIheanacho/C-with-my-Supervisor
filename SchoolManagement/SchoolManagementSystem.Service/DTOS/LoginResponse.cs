namespace SchoolManagementSystem.Service.DTOS
{
    public record LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
