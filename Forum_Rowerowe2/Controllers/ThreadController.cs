using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForumRowerowe.Data;
using ForumRowerowe.Models;
using Microsoft.AspNetCore.Authorization;

namespace ForumRowerowe.Controllers
{
    public class ThreadController : Controller
    {
        private IForumThreadCrudRepository repository;
        public ThreadController(IForumThreadCrudRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            var allThreads = repository.FindAll();
            return View(allThreads);
        }

        // GET: Threads/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreadID,Title")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                var currentUserName = User.Identity.Name;
                thread.authorID = currentUserName;
                repository.AddThreads(thread);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Threads/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var thread = repository.FindThread(id);
            var currentUserName = User.Identity.Name;
            if (thread.authorID == currentUserName)
            {
                return View(thread);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ThreadID,Title")] Thread thread)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUserName = User.Identity.Name;
                    thread.authorID = currentUserName;
                    repository.UpdateThreads(thread);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreadExists(thread.ThreadID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(thread);
            //return RedirectToAction(nameof(Index));
        }

        // GET: Threads/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int Id = id.GetValueOrDefault();
            var thread = repository.FindThread(Id);
            var currentUserName = User.Identity.Name;
            if (thread.authorID == currentUserName)
            {
                return View(thread);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Threads/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                repository.DeleteThreads(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ThreadExists(int id)
        {
            //return _context.Threads.Any(e => e.ThreadID == id);
            if(repository.FindThread(id) != null)
            {
                return true;
            }
            return false;
        }
        // GET: Threads/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thread = await _context.Threads
                .FirstOrDefaultAsync(m => m.ThreadID == id);
            if (thread == null)
            {
                return NotFound();
            }

            return View(thread);
        }*/
        [Route("/api/threads/")]
        public List<Thread> GetThreads()
        {
            return repository.FindAll().ToList();
        }

        [Route("/api/threads/{id}")]
        public Thread GetThread(int id)
        {
            return repository.FindThread(id);
        }
    }
}
