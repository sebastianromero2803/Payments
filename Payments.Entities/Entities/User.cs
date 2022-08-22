using System.ComponentModel.DataAnnotations;

namespace Payments.Entities.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogIn { get; set; }
        public DateTime? LastLogOut { get; set; }
        public string? Token { get; set; }
        public string Status { get; set; }
    }
}
