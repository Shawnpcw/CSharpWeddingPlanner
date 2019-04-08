using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int userid {get;set;}

        [Required]
        [MinLength(2)]
        public string firstName {get;set;}
        [Required]
        [MinLength(2)]
        public string lastName {get;set;}

        [EmailAddress]
        [Required]
        [MinLength(2)]
        public string email { get; set; }
        [Required]
        [MinLength(2, ErrorMessage="Password must be 8 characters or longer!")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [NotMapped]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string passwordConfirm { get; set; }
        
        public List<Guest> Guests {get;set;}
        
    }
    public class LoginUser
    {
        [Required]
        public string email {get; set;}
        [Required]
        public string password { get; set; }
    }
}