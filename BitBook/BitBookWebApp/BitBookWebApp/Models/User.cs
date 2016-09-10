using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitBookWebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide your name")]
        [StringLength(10, MinimumLength = 3)]
        [DisplayName("Employee Name")]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])", ErrorMessage = "Please provide valid email")]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}