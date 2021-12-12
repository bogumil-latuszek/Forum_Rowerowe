using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Data
{
    public class ForumAdminRepository: IForumCrudRepository
    {
        private ApplicationDbContext _context;
        public ForumAdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DeletePosts(int postID)
        {
            _context.Posts.Remove(FindPost(postID));
            _context.SaveChanges();
        }
        public Post FindPost(int id)
        {
            var post = (from x in _context.Posts where x.PostID == id select x).First();
            return post;
        }
        public void AddPosts(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public void UpdatePosts(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
        public IList<Post> FindAll()
        {
            return _context.Posts.ToList();
        }
        public IList<Post> FindPage(int page, int size)
        {
            return (from p in _context.Posts select p).OrderBy(p => p.PostID).Skip((page - 1) * size).Take(size).ToList();
        }
    }
}
