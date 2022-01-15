using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumRowerowe.Models
{
    public class Post
    {      
        [HiddenInput]
        public int PostID { get; set; }

        [MaxLength(length: 255, ErrorMessage = "Twoja wiadomość jest za długa")]
        [MinLength(length: 1, ErrorMessage = "Twoja wiadomość jest za krótka")]
        [Display(Name ="Twój post")]
        public string Content { get; set; }
        public string authorID { get; set; }
        [HiddenInput]
        public int ThreadID { get; set; }
        public Thread Thread { get; set; } //navigation property

        [HiddenInput]
        [ForeignKey("Image")]
        public int? ImageID { get; set; }
    }
}
