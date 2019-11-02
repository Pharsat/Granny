using System.ComponentModel.DataAnnotations;

namespace Granny.DataTransferObject
{
    public class AuthenticateDto
    {
        [Required]
        public string GoogleToken { get; set; }
    }
}
