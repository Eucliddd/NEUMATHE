using System.Threading.Tasks;

namespace Mathe.Services
{
    /// <summary>
    /// 内容导航服务接口
    /// </summary>
    public interface IContentNavigationService
    {
        /// <summary>
        /// 异步导航
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <returns></returns>
        Task NavigateToAsync(string pageKey);

        /// <summary>
        /// 带参数异步导航
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        Task NavigateToAsync(string pageKey, object parameter);

        /// <summary>
        /// 弹出导航栈的顶端页面
        /// </summary>
        /// <returns></returns>
        Task PopAsync();


        /// <summary>
        /// 内容导航常数
        /// </summary>
        public static class ContentNavigationConstants
        {
            public static readonly string TreePage = nameof(Views.TreePage);
            public static readonly string ProblemPage = nameof(Views.ProblemPage);
            public static readonly string ZoomPage = nameof(Views.ZoomPage);
        }
    }
}