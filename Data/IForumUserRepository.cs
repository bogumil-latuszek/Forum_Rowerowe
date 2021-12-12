using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Data
{
    interface IForumUserRepository
    {
        Post FindPost(int postID);
        IList<Post> FindByContent(string contentPattern);
        IList<Post> FindPage(int page, int size);
    }
}
