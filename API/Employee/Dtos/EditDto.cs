using Employee.Domain.Enum;

namespace Employee.API.Dtos
{
    public class EditDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public RoleType RoleType { get; set; }
    }
}
