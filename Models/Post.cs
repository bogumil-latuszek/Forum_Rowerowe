using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ForumRowerowe.Models
{
    public class Post
    {
        [HiddenInput]
        public int PostID { get; set; }
        [MaxLength(length: 255, ErrorMessage = "Twoja wiadomość jest za długa")]
        [MinLength(length: 1, ErrorMessage = "Twoja wiadomość jest za krótka")]
        public string Content { get; set; }


    }
}
