using System.ComponentModel.DataAnnotations;

namespace Chat.ViewModels
{
    public class RestUserRegistrationInfo
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
