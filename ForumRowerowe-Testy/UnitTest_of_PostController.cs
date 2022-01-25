using ForumRowerowe.Controllers;
using ForumRowerowe.Data;
using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ForumRowerowe_Testy
{
    public class UnitTest_of_PostController
    {

        [Fact]
        public void Test_GetPosts_returns_all_posts()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);
            int expectedSize = 4;

            //When
            List<Post> outputPosts = myController.GetPosts();
            //Then
            Assert.Equal(outputPosts.Count, expectedSize);
            Assert.Equal(outputPosts, repo.FindAll());

        }
    }
}
