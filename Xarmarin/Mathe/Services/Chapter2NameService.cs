using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mathe.Services
{
    /// <summary>
    /// 章节ID转章节名字服务
    /// </summary>
    public class Chapter2NameService : IChapter2NameService
    {
        /// <summary>
        /// 章节名节点
        /// </summary>
        private class ChapterName
        {
            /// <summary>
            /// 章节ID
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// 章节名字
            /// </summary>
            public string text { get; set; }
        }

        /// <summary>
        /// 是否初始化
        /// </summary>
        private bool IsInit = false;

        /// <summary>
        /// 缓存结果
        /// </summary>
        private Hashtable _map = new Hashtable();

        /// <summary>
        /// 异步初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            if (IsInit) return;
            const string path = "chapter2Name.json";
            await Helper.CopyStreamFromEmbedded(path);
            var reader = new StreamReader(new FileStream(Helper.GetResourcePath(path), FileMode.Open, FileAccess.Read));
            var json = await reader.ReadToEndAsync();
            var list = JsonConvert.DeserializeObject<List<ChapterName>>(json);
            foreach (var t in list)
            {
                _map[t.id] = t.text;
            }

            IsInit = true;
        }

        /// <summary>
        /// 章节ID转章节名字
        /// </summary>
        /// <param name="chapter">章节ID</param>
        /// <returns>章节名称</returns>
        public string Chapter2Name(string chapter)
        {
            if (!_map.ContainsKey(chapter))
            {
                return null;
            }

            return _map[chapter] as string;
        }
    }
}