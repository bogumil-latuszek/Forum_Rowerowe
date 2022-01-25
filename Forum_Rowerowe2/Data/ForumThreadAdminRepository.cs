using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Data
{

    public class ForumThreadAdminRepository : IForumThreadCrudRepository
    {
        private ApplicationDbContext _context;
        public ForumThreadAdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DeleteThreads(int threadID)
        {
            _context.Threads.Remove(FindThread(threadID));
            _context.SaveChanges();
        }
        public Thread FindThread(int id)
        {
            var thread = (from x in _context.Threads where x.ThreadID == id select x).First();
            return thread;
        }
        public void AddThreads(Thread thread)
        {
            _context.Threads.Add(thread);
            _context.SaveChanges();
        }
        public void UpdateThreads(Thread thread)
        {
            _context.Threads.Update(thread);
            _context.SaveChanges();
        }
        public IList<Thread> FindAll()
        {
            return _context.Threads.ToList();
        }
        public IList<Thread> FindPage(int page, int size)
        {
            return (from t in _context.Threads select t).OrderBy(t => t.ThreadID).Skip((page - 1) * size).Take(size).ToList();
        }
    }
}