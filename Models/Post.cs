using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ForumRowerowe.Models
{
    public class Post
    {
        public string authorID;
        [HiddenInput]
        public int PostID { get; set; }

        [MaxLength(length: 255, ErrorMessage = "Twoja wiadomość jest za długa")]
        [MinLength(length: 1, ErrorMessage = "Twoja wiadomość jest za krótka")]
        [Display(Name ="Twój post")]
        public string Content { get; set; }
        public Thread Thread { get; set; } //navigation property
    }
}
