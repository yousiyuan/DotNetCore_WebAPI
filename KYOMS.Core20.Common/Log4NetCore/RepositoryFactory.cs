using log4net.Core;
using log4net.Repository.Hierarchy;
using System.Linq;

namespace KYOMS.Core20.Common.Log4NetCore
{
    /// <summary>
    /// 实现全局共享同一个Logger容器
    /// </summary>
    internal static class RepositoryFactory
    {
        private static readonly object LockObj = new object();
        internal static Hierarchy GetRepository(string repositoryname)
        {
            var repositories = LoggerManager.GetAllRepositories();
            var repository = repositories.FirstOrDefault(t => t.Name == repositoryname);
            if (repository == null)
                lock (LockObj)
                {
                    repositories = LoggerManager.GetAllRepositories();
                    repository = repositories.FirstOrDefault(t => t.Name == repositoryname);
                    if (repository == null)
                        repository = LoggerManager.CreateRepository(repositoryname);
                }
            return repository as Hierarchy;
        }
    }
}
