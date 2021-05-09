using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mathe.Services
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public ObservableCollection<TreeNode> children { get; set; }
    }

    /// <summary>
    /// 树服务接口
    /// </summary>
    public interface ITreeService
    {
        /// <summary>
        /// 异步初始化
        /// </summary>
        /// <returns></returns>
        public Task InitializeAsync();

        /// <summary>
        /// 获取树
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<TreeNode> GetTree();
    }
}