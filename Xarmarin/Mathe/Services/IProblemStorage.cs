using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mathe.Models;

namespace Mathe.Services
{
    /// <summary>
    /// 数据库服务接口
    /// </summary>
    public interface IProblemStorage
    {
        /// <summary>
        /// 异步初始化
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        /// 是否已初始化
        /// </summary>
        /// <returns></returns>
        bool IsInitialized();

        /// <summary>
        /// 根据ID异步获取数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<Problem> GetProblemAsync(int id);

        /// <summary>
        /// 根据父节点ID获取数据集
        /// </summary>
        /// <param name="parent">父节点ID</param>
        /// <param name="mark">是否标记</param>
        /// <param name="done">是否完成</param>
        /// <returns>ProblemList</returns>
        Task<IList<Problem>> GetProblemsByParentAsync(string parent, int mark, int done);

        /// <summary>
        /// 根据父节点ID获取数据集
        /// </summary>
        /// <param name="parent">父节点ID</param>
        /// <returns>ProblemList</returns>
        public Task<IList<Problem>> GetProblemsByParentAsync(string parent);

        /// <summary>
        /// 异步获取数据集
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="skip">跳过多少</param>
        /// <param name="take">最大多少</param>
        /// <returns>ProblemList</returns>
        Task<IList<Problem>> GetProblemsAsync(Expression<Func<Problem, bool>> where, int skip, int take);

        /// <summary>
        /// 根据ID更新数据库
        /// </summary>
        /// <param name="problem"></param>
        /// <returns></returns>
        public Task UpdateProblemsAsync(Problem problem);

        /// <summary>
        /// 异步关闭
        /// </summary>
        /// <returns></returns>
        Task CloseAsync();
    }

    /// <summary>
    /// 数据库服务常数
    /// </summary>
    public static class ProblemStorageConstants
    {
        /// <summary>
        /// 版本
        /// </summary>
        public const int Version = 1;

        /// <summary>
        /// 版本键
        /// </summary>
        public const string VersionKey = nameof(ProblemStorageConstants) + "." + nameof(Version);
    }
}