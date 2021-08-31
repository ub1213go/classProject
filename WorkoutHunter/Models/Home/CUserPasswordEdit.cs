using System.ComponentModel.DataAnnotations;

namespace WorkoutHunterV2.Models.Home
{
    public class CUserPasswordEdit
    {
        public string UID { get; set; }
        public string Key { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9_]*")]
        public string Password { get; set; }
    }
}
