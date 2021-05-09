using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Mathe.Services
{
    /// <summary>
    /// 内容页激活服务
    /// </summary>
    public class ContentPageActivationService : IContentPageActivationService
    {
        /// <summary>
        /// 页面键字符串转页面字典
        /// </summary>
        private Dictionary<string, Type> PageKeyTypeDictionary =
            new Dictionary<string, Type>()
            {
                [IContentNavigationService.ContentNavigationConstants.TreePage] = typeof(Views.TreePage),
                [IContentNavigationService.ContentNavigationConstants.ProblemPage] = typeof(Views.ProblemPage),
                [IContentNavigationService.ContentNavigationConstants.ZoomPage] = typeof(Views.ZoomPage)
            };

        /// <summary>
        /// 缓存
        /// </summary>
        private Dictionary<string, ContentPage> cache = new Dictionary<string, ContentPage>();

        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <returns>页面</returns>
        public ContentPage Activate(string pageKey) => cache.ContainsKey(pageKey)
            ? cache[pageKey]
            : cache[pageKey] = Activator.CreateInstance(PageKeyTypeDictionary[pageKey]) as ContentPage;
    }
}