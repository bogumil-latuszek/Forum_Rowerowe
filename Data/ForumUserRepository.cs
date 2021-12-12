using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Data
{
    public class ForumUserRepository: IForumUserRepository
    {
        private ApplicationDbContext _context;
        public ForumUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Post FindPost(int id)
        {
            var item = (from x in _context.Posts where x.PostID == id select x).First();
            return item;
        }
        public IList<Post> FindByContent(string contentPattern)
        {
                return (from p in _context.Posts where p.Content.Contains(contentPattern) select p).ToList();
        }
        public IList<Post> FindPage(int page, int size)
        {
                return (from p in _context.Posts select p).OrderBy(p => p.PostID).Skip((page - 1) * size).Take(size).ToList();
        }
    }
}
