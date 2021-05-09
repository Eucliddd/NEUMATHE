using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Mathe.Models;
using SQLite;

namespace Mathe.Services
{
    /// <summary>
    /// 数据库服务
    /// </summary>
    public class ProblemStorage : IProblemStorage
    {
        public ProblemStorage(IPreferenceStorage preferenceStorage)
        {
            _preferenceStorage = preferenceStorage;
        }

        /// <summary>
        /// 根据ID异步获取数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<Problem> GetProblemAsync(int id) =>
            await Connection.Table<Problem>().FirstOrDefaultAsync(p => p.id == id);

        /// <summary>
        /// 异步初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            if (IsInit) return;
            await Helper.CopyStreamFromEmbedded(DbName);
            _preferenceStorage.Set(ProblemStorageConstants.VersionKey, ProblemStorageConstants.Version);
            IsInit = true;
        }

        /// <summary>
        /// 是否初始化
        /// </summary>
        private bool IsInit = false;

        /// <summary>
        /// 是否已初始化
        /// </summary>
        /// <returns></returns>
        public bool IsInitialized() => IsInit;

        /// <summary>
        /// 异步关闭
        /// </summary>
        /// <returns></returns>
        public Task CloseAsync()
        {
            return Connection.CloseAsync();
        }

        /// <summary>
        /// 根据父节点ID获取数据集
        /// </summary>
        /// <param name="parent">父节点ID</param>
        /// <param name="mark">是否标记</param>
        /// <param name="done">是否完成</param>
        /// <returns>ProblemList</returns>
        public async Task<IList<Problem>> GetProblemsByParentAsync(string parent, int mark, int done) =>
            await Connection
                .Table<Problem>().Where(p => p.chapter == parent && p.mark == mark && p.done == done).Skip(0)
                .Take(Int32.MaxValue).ToListAsync();

        /// <summary>
        /// 根据父节点ID获取数据集
        /// </summary>
        /// <param name="parent">父节点ID</param>
        /// <returns>ProblemList</returns>
        public async Task<IList<Problem>> GetProblemsByParentAsync(string parent) =>
            await Connection
                .Table<Problem>().Where(p => p.chapter == parent).Skip(0)
                .Take(Int32.MaxValue).ToListAsync();

        /// <summary>
        /// 异步获取数据集
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="skip">跳过多少</param>
        /// <param name="take">最大多少</param>
        /// <returns>ProblemList</returns>
        public async Task<IList<Problem>>
            GetProblemsAsync(Expression<Func<Problem, bool>> @where, int skip, int take) =>
            await Connection.Table<Problem>().Where(where).Skip(skip).Take(take).ToListAsync();

        /// <summary>
        /// 根据ID更新数据库
        /// </summary>
        /// <param name="problem"></param>
        /// <returns></returns>
        public async Task UpdateProblemsAsync(Problem problem)
        {
            await Connection.InsertOrReplaceAsync(problem);
        }

        /// <summary>
        /// 数据库文件路径
        /// </summary>
        public static readonly string ProblemDbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder
                .LocalApplicationData), DbName);

        private IPreferenceStorage _preferenceStorage;

        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Connection =>
            _connection ??= new SQLiteAsyncConnection(ProblemDbPath);

        /// <summary>
        /// 数据库名称
        /// </summary>
        private const string DbName = "hm.db";
    }
}