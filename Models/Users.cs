using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace The_Wall.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Email:     ")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password:        ")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        [MaxLength(255)]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // Will not be mapped to your users table!
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password:")]
        public string Confirm { get; set; }




        public List<Messages> CreatedMessages { get; set; }

        public List<Comments> CreatedComments { get; set; }
    }
}