using System;
using System.Threading.Tasks;
using Mathe.Views;
using Xamarin.Forms;

namespace Mathe.Services
{
    public class ContentNavigationService : IContentNavigationService
    {
        /// <summary>
        /// 内容页面激活服务。
        /// </summary>
        private IContentPageActivationService contentPageActivationService;

        /// <summary>
        /// 导航工具。
        /// </summary>
        private MainPage MainPage =>
            _mainPage ??= Application.Current.MainPage as MainPage;

        private MainPage _mainPage;

        public ContentNavigationService(IContentPageActivationService contentPageActivationService)
        {
            this.contentPageActivationService = contentPageActivationService;
        }

        /// <summary>
        /// 异步导航
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <returns></returns>
        public async Task NavigateToAsync(string pageKey)
        {
            await Application.Current.MainPage.Navigation.PushAsync(contentPageActivationService.Activate(pageKey));
        }

        /// <summary>
        /// 带参数异步导航
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public async Task NavigateToAsync(string pageKey, object parameter)
        {
            var page = contentPageActivationService.Activate(pageKey);
            NavigationContext.SetParameter(page, parameter);
            await Application.Current.MainPage.Navigation.PushAsync(contentPageActivationService.Activate(pageKey));
        }

        /// <summary>
        /// 弹出导航栈的顶端页面
        /// </summary>
        /// <returns></returns>
        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}