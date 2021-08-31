using System.ComponentModel.DataAnnotations;

namespace WorkoutHunterV2.Models.Home
{
    public class User
    {
        public string UID { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9_]*@gmail.com")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9_]*")]
        public string Password { get; set; }
    }
}
