using System.ComponentModel.DataAnnotations;

namespace TimesheetPlayground.UI.Models
{
    public class AuthUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
