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

        [Fact]
        public void Test_CreatePost_correctly_writes_post_to_database()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);
            Post post = new Post() { Content = "ok", ThreadID = 3 };

            //When
            var response = myController.CreatePost(post);

            //Then
            var action_result = Assert.IsType<CreatedResult>(response);
            Assert.Equal(action_result.Value, repo.FindPost(5));
        }

        [Fact]
        public void Test_CreatePost_returns_BadRequest_on_incorect_input()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);

            //When
            var response = myController.CreatePost(null);

            //Then
            var action_result = Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Test_EditPost_correctly_edits_post_in_database()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);
            string new_content = "ok";
            int new_threadID = 3;
            Post post = new Post() { Content = new_content, ThreadID = new_threadID };

            //When
            var response = myController.EditPost(post, 4);

            //Then
            var action_result = Assert.IsType<CreatedResult>(response);
            var post4inRepo = repo.FindPost(4);
            Assert.Equal(action_result.Value, post4inRepo);
            Assert.Equal(post4inRepo.Content, new_content);
            Assert.Equal(post4inRepo.ThreadID, new_threadID);
        }

        [Fact]
        public void Test_DeletePost_correctly_deletes_post_from_database()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);

            //When
            var response = myController.DeletePost(4);

            //Then
            Assert.IsType<OkObjectResult>(response);
            Assert.Null(repo.FindPost(4));
        }

        [Fact]
        public void Test_DeletePost_returns_BadRequest_on_incorect_input()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);

            //When
            var response = myController.DeletePost(5);

            //Then
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void Test_EditPost_returns_BadRequest_on_incorrect_input()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);

            //When
            var response = myController.EditPost(null, 4);

            //Then
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void Test_EditPost_returns_NotFound_when_given_id_not_in_database()
        {
            //Given
            IForumCrudRepository repo = new MemoryPostsRepository();
            repo.AddPosts(new Post { Content = "c1", ThreadID = 1 });
            repo.AddPosts(new Post { Content = "c2", ThreadID = 2 });
            repo.AddPosts(new Post { Content = "c3", ThreadID = 3 });
            repo.AddPosts(new Post { Content = "c4", ThreadID = 4 });
            PostController myController = new PostController(repo);
            Post post = new Post() { Content = "c4", ThreadID = 4 };

            //When
            var response = myController.EditPost(post, 5);

            //Then
            Assert.IsType<NotFoundResult>(response);
        }
    }
}
