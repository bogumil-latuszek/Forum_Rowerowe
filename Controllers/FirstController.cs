using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Controllers
{
    public class FirstController : Controller
    {
        private static int counter = 3;
        private static List<ForumRowerowe.Models.Post> ListOfPosts =
        new List<ForumRowerowe.Models.Post>()
        {
            new Models.Post(){
            PostID=1,ThreadID=1,Content= "cześć właśnie kupiłem rower" },
            new Models.Post(){
            PostID=2,ThreadID=1,Content= "jaki?" },
            new Models.Post(){
            PostID=2,ThreadID=1,Content= "Scott górski z 2015 mało używany, " +
                "całkiem tanio idzie znaleść podobne na wyprzedażach tutaj w Lublinie" }
        };
        public IActionResult Index()
        {
            return View("ShowAllPosts", ListOfPosts);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}
