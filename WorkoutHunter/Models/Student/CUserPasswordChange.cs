using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutHunterV2.Models.Student
{
    public class CUserPasswordChange
    {
        public string UID { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9_]*")]
        public string Password { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9_]*")]
        public string rePassword { get; set; }
    }
}
