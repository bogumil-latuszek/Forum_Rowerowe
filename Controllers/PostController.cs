using ForumRowerowe.Data;
using ForumRowerowe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult Index(int threadID, string threadTitle)
        {
            TempData["ThreadID"] = threadID;
            TempData["ThreadTitle"] = threadTitle;
            var listp = repository.FindPosts(threadID);
            return View(listp);
        }
        [Authorize]
        // GET: Post/Create
        public IActionResult Create(int threadID, string threadTitle)
        {
            TempData["ThreadID"] = threadID;
            TempData["ThreadTitle"] = threadTitle;
            return View();
        }
        // POST: Post/Create
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("PostID,Content,ThreadID")] Models.Post post, string threadTitle, IFormFile picture)
        {
            var currentUserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                post.authorID = currentUserName;
                repository.AddPosts(post);
            }

            if (picture != null)
            {
                AddPicture(currentUserName, picture, post);
            }

            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID, threadTitle = threadTitle });
        }

        private void AddPicture(string currentUserName, IFormFile picture, Post post)
        {
            string cwd = Directory.GetCurrentDirectory();
            string path = cwd + "\\Images\\" + currentUserName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = picture.FileName;
            path = Path.Combine(path, fileName);
            using (FileStream fs = System.IO.File.Create(path))
            {
                picture.CopyTo(fs);
                Image image = new Image();
                image.ImagePath = path;
                repository.AddImage(image, post);

            }

            return;

        }

        // GET: Post/Delete
        [Authorize]
        public async Task<IActionResult> Delete(int? id, string threadTitle)
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
                TempData["ThreadTitle"] = threadTitle;
                return View(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID, threadTitle = threadTitle });
        }

        // POST: Post/Delete/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id, string threadTitle)
        {
            var post = repository.FindPost(id);
            var threadID = post.ThreadID;
            if (ModelState.IsValid)
            {
                repository.DeletePosts(id);
            }
            return RedirectToAction(nameof(Index), new { threadID = threadID, threadTitle = threadTitle });
        }

        // GET: Post/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id, string threadTitle)
        {
            var post = repository.FindPost(id);
            var currentUserName = User.Identity.Name;
            if (post.authorID == currentUserName)
            {
                TempData["ThreadTitle"] = threadTitle;
                return View(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID, threadTitle = threadTitle });
        }

        // POST: Post/Edit/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit( [ Bind("PostID,Content,ThreadID")] Models.Post post, string threadTitle)
        {
            post.authorID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                repository.UpdatePosts(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID, threadTitle = threadTitle });
        }
    }
}
