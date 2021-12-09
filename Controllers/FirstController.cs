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
            PostID=1,Content= "cześć właśnie kupiłem rower" },
            new Models.Post(){
            PostID=2,Content= "jaki?" },
            new Models.Post(){
            PostID=3,Content= "Scott górski z 2015 mało używany, " +
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
        // POST: Post/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("PostID,Content")] Models.Post post)
        {
            if (ModelState.IsValid)
            {
                counter++;
                post.PostID = counter;
                ListOfPosts.Add(post);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
            //return View("ShowAllPosts");
        }

        // GET: Post/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            foreach (var post in ListOfPosts)
            {
                if (post.PostID == id)
                {
                    return View();
                }
            }
            return NotFound();
        }

        // POST: Post/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int? removedPostID = null;
            foreach (var post in ListOfPosts)
            {
                if (post.PostID == id)
                {
                    removedPostID = post.PostID;
                    ListOfPosts.Remove(post);
                    if (counter>0)
                    {
                        counter--;
                    }
                    break; //dodać zabezpieczenie na wypadek gdyby 2 lub więcej postów miało to samo postID
                }
                
            }
            if (removedPostID == null) { return RedirectToAction(nameof(Index)); }
            foreach (var post in ListOfPosts)
            {
                if (post.PostID >= removedPostID && post.PostID >= 1)
                {
                    post.PostID -= 1;
                }

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
             return View(ListOfPosts[id-1]);
        }

        // POST: Post/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit( [ Bind("PostID,Content")] Models.Post post)
        {

            if (ModelState.IsValid)
            {
                for (int i = 0; i < ListOfPosts.Count; i++)
                {
                    if(ListOfPosts[i].PostID == post.PostID)
                    {
                        ListOfPosts[i] = post;
                        break;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
