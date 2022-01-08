using ForumRowerowe.Data;
using ForumRowerowe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Controllers
{
    public class PostController : Controller
    {
        private IForumCrudRepository repository;
        public PostController(IForumCrudRepository repository)
        {
            this.repository = repository;
        }        
        public IActionResult Index(int threadID)
        {
            TempData["ThreadID"] = threadID;
            var listp = repository.FindPosts(threadID);
            return View(listp);
        }
        [Authorize]
        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
         }
        // POST: Post/Create
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("PostID,Content")] Models.Post post)
        {
            if (ModelState.IsValid)
            {
                var currentUserName = User.Identity.Name;
                post.authorID = currentUserName;
                repository.AddPosts(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID });
        }

        // GET: Post/Delete
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int Id = id.GetValueOrDefault();
            var post = repository.FindPost(Id);
            var currentUserName = User.Identity.Name;
            if (post.authorID == currentUserName)
            {
                return View(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID });
        }

        // POST: Post/Delete/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                repository.DeletePosts(id);
            }
            var post = repository.FindPost(id);
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID });
        }

        // GET: Post/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var post = repository.FindPost(id);
            var currentUserName = User.Identity.Name;
            if (post.authorID == currentUserName)
            {
                return View(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID });
        }

        // POST: Post/Edit/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit( [ Bind("PostID,Content,ThreadID")] Models.Post post)
        {
            post.authorID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                repository.UpdatePosts(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID });
        }
    }
}
