using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace The_Wall.Models
{
    public class Messages
    {
        [Key]
        public int MessageID { get; set; }

        public int UserID { get; set; }

        [Display(Name = "Post a message: ")]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Users Message_Creator { get; set; }

        public List<Comments> Message_Comments { get; set; }
    }
}