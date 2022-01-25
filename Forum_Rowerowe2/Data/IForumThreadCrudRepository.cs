using ForumRowerowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumRowerowe.Data
{
    public interface IForumThreadCrudRepository
    {
        void DeleteThreads(int threadID);
        void AddThreads(Thread thread);
        Thread FindThread(int threadID);
        void UpdateThreads(Thread thread);
        IList<Thread> FindAll();
        IList<Thread> FindPage(int page, int size);
    }
}