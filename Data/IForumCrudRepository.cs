using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Data
{
    public interface IForumCrudRepository
    {
        void DeletePosts(int postID);
        void AddPosts(Post post);
        Post FindPost(int postID);
        void UpdatePosts(Post post);
        IList<Post> FindAll();
        IList<Post> FindPage(int page, int size);
    }
}
