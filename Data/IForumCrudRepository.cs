using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Data
{
    public interface IForumCrudRepository
    {
        //this may be later moved to its own repository
        void AddImage(Image image, Post post);
        #nullable enable
        Image? FindImage(int imageID);
        #nullable disable
        void DeleteImage(int imageID);
        //*********************************************
        void DeletePosts(int postID);
        void AddPosts(Post post);
        Post FindPost(int postID);
        void UpdatePosts(Post post);
        IList<Post> FindAll();
        IList<Post> FindPage(int page, int size);
        IList<Post> FindPosts(int threadID);
    }
}
