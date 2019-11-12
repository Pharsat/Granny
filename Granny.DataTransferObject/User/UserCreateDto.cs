using System.ComponentModel.DataAnnotations;

namespace Granny.DataTransferObject.User
{
    public class UserCreateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
