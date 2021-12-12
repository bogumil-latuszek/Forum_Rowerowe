using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Models
{
    public class Thread
    {
        [HiddenInput]
        public int ThreadID { get; set; }

        [MaxLength(length: 255, ErrorMessage = "za długi tytuł")]
        [MinLength(length: 2, ErrorMessage = "za krótki tytuł")]
        [Display(Name = "Tytuł wątku")]
        public string Title { get; set; }
        public ICollection<Post> Posts { get; set; } //navigation property
        public Thread()
        {
            Posts = new HashSet<Post>();
        }
    }
}
