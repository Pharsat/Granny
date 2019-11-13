using System.ComponentModel.DataAnnotations;

namespace Granny.DataTransferObject.User
{
    public class UserCreateDto
    {
        [Required]
        public string GoogleToken { get; set; }
    }
}
