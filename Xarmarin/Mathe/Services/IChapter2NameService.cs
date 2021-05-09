using System.Threading.Tasks;

namespace Mathe.Services
{
    /// <summary>
    /// 章节ID转章节名称服务接口
    /// </summary>
    public interface IChapter2NameService
    {
        /// <summary>
        /// 章节ID转章节名字
        /// </summary>
        /// <param name="chapter">章节ID</param>
        /// <returns>章节名称</returns>
        public string Chapter2Name(string chapter);

        /// <summary>
        /// 异步初始化
        /// </summary>
        /// <returns></returns>
        public Task InitializeAsync();
    }
}