using System.ComponentModel.DataAnnotations;

namespace Vaajak.Application.Dto.Account
{
    public class GoogleSigninDto
    {
        [Required]
        public string IdToken { get; set; } = string.Empty;
    }
}
