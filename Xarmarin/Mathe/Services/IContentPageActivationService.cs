using Xamarin.Forms;

namespace Mathe.Services
{
    /// <summary>
    /// 内容页激活服务接口
    /// </summary>
    public interface IContentPageActivationService
    {
        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <returns>页面</returns>
        ContentPage Activate(string pageKey);
    }
}