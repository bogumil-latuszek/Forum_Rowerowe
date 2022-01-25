using Microsoft.AspNetCore.Mvc;

namespace ForumRowerowe.Models
{
    public class Image
    {
        [HiddenInput]
        public int ImageID { get; set; }

        public string ImagePath { get; set; }

    }
}
