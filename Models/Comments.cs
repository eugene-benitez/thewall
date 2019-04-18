using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace The_Wall.Models
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }

        public int MessageID { get; set; }

        public int UserID { get; set; }



        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public Users Comment_Creator { get; set; }

        public Messages Parent_Message { get; set; }
    }
}