using ForumRowerowe.Controllers;
using ForumRowerowe.Data;
using ForumRowerowe.Models;
using Microsoft.AspNetCore.Mvc;
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
        [Fact]
        public void Test_GetPost_returns_requested_post()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);

            //When
            var response = myController.GetPost(3);

            //Then
            var action_result = Assert.IsType<ActionResult<Post>>(response);
            var result = Assert.IsType<OkObjectResult>(action_result.Result);
            Assert.Equal(repo.FindPost(3), result.Value);
        }
        [Fact]
        public void Test_GetPost_returns_NotFound_for_requested_post_not_in_repository()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);

            //When
            var response = myController.GetPost(7);

            //Then
            var action_result = Assert.IsType<ActionResult<Post>>(response);
            Assert.IsType<NotFoundResult>(action_result.Result);
        }
    }
}
