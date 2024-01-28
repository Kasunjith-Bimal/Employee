using Employee.Domain.Enum;

namespace Employee.API.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public RoleType RoleType { get; set; }
    }
}
