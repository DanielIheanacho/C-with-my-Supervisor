namespace SchoolManagement.Web.DTOs
{
    public record RegisterSchoolRequest
    {
        public required string SchoolName { get; set;} 
        public required string AdminFirstName { get; set; }
        public required string AdminLastName { get; set; }
        public required string AdminEmail {get; set; }
        public required string AdminPassWord { get; set; }
    }
}
