using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Data
{
    public class MemoryPostsRepository : IForumCrudRepository
    {
        private Dictionary<int, Post> posts = new Dictionary<int, Post>();
        private int index = 1;
        private int nextIndex() 
        {
            return index++;
        }
        public void AddImage(Image image, Post post)
        {
            throw new NotImplementedException();
        }

        public void AddPosts(Post post)
        {
            post.PostID = nextIndex();
            posts.Add(post.PostID, post);
        }

        public void DeleteImage(int imageID)
        {
            throw new NotImplementedException();
        }

        public void DeletePosts(int postID)
        {
            var postToDel = FindPost(postID);
            if (postToDel != null)
            {
                posts.Remove(postID);
            }
        }

        public IList<Post> FindAll()
        {
            return posts.Values.ToList();
        }

        public Image FindImage(int imageID)
        {
            throw new NotImplementedException();
        }

        public IList<Post> FindPage(int page, int size)
        {
            throw new NotImplementedException();
        }
        #nullable enable
        public Post? FindPost(int postID)
        {
            if (posts.ContainsKey(postID))
            {
                return posts[postID];
            }
            return null;
        }
        #nullable disable
        public IList<Post> FindPosts(int threadID)
        {
            List<Post> threadPosts = new List<Post>();
            foreach(var pair in posts)
            {
                if (pair.Value.ThreadID == threadID)
                {
                    threadPosts.Add(pair.Value);
                }
            }
            return threadPosts;
        }

        public void UpdatePosts(Post post)
        {
            var postToDel = FindPost(post.PostID);
            if (postToDel != null)
            {
                posts[post.PostID] = post;
            }
        }
    }
}
