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
    public class PostWithImg
    {
        public Post Post;
        #nullable enable
        public string? ImagePath;
        public PostWithImg(Post post, string? imagePath)
        {
            Post = post;
            ImagePath = imagePath;
        }
        #nullable disable
    }

    
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
            //this should be in a separate method in its own repository
            //var list = new List<Tuple<Post, string? >>();
            var listPI = new List<PostWithImg>();
            foreach (Post post in listp)
            {
                string? imgPath = null;
                if (post.ImageID != null)
                {
                    #nullable enable
                    Image? img = repository.FindImage((int)post.ImageID);
                    if(img != null)
                    {
                        imgPath = img.ImagePath;
                    }    
                    #nullable disable
                }
                listPI.Add(new PostWithImg(post, imgPath));
            }
            //*************************************
            return View(listPI);
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
            string path = cwd + "\\wwwroot\\Images\\" + currentUserName;
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
                string html_path = "/Images/" + currentUserName + "/" + fileName;
                image.ImagePath = html_path;
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
                TempData["ImageID"] = post.ImageID;
                return View(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID, threadTitle = threadTitle });
        }

        // POST: Post/Edit/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit( [ Bind("PostID,Content,ThreadID,ImageID")] Models.Post post, string threadTitle, IFormFile picture, bool deletePicture)
        {
            post.authorID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if(picture != null)
                {
                    AddPicture(post.authorID, picture, post);
                }
                if (deletePicture == true)
                {
                    post.ImageID = null;
                }
                repository.UpdatePosts(post);
            }
            return RedirectToAction(nameof(Index), new { threadID = post.ThreadID, threadTitle = threadTitle });
        }
        [Route("/api/posts/")]
        public List<Post> GetPosts()
        {
            return repository.FindAll().ToList();
        }

        [Route("/api/posts/{id}")]
        public Post GetPosts(int id)
        {
            return repository.FindPost(id);
        }
    }
}
