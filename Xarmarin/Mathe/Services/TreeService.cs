using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Mathe.Services;
using Newtonsoft.Json;

namespace Mathe.Services
{
    /// <summary>
    /// 树服务
    /// </summary>
    public class TreeService : ITreeService
    {
        private ObservableCollection<TreeNode> _treeNode;
        private IProblemStorage _problemStorage;
        private bool IsInit = false;

        public TreeService(IProblemStorage problemStorage)
        {
            _problemStorage = problemStorage;
        }

        /// <summary>
        /// 获取树
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<TreeNode> GetTree()
        {
            return _treeNode;
        }

        /// <summary>
        /// 异步初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            if (_treeNode == null && !IsInit)
            {
                string path = "chapterTree.json";
                await Helper.CopyStreamFromEmbedded(path);
                var stream = new FileStream(Helper.GetResourcePath(path), FileMode.Open, FileAccess.Read);
                var streamReader = new StreamReader(stream);
                var json = await streamReader.ReadToEndAsync();
                _treeNode = JsonConvert.DeserializeObject<ObservableCollection<TreeNode>>(json);
                IsInit = true;
            }
        }
    }
}