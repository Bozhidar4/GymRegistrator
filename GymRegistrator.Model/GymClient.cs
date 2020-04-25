using System.ComponentModel.DataAnnotations;

namespace GymRegistrator.Model
{
    public class GymClient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
