using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Mathe.Services;
using Xamarin.Forms;

namespace Mathe.ViewModels
{
    /// <summary>
    /// 放大页面视图模型
    /// </summary>
    public class ZoomPageViewModel : ViewModelBase
    {
        public ZoomPageViewModel(IContentNavigationService contentNavigationService)
        {
            _contentNavigationService = contentNavigationService;
            _tapCommand = new Command(async () => await OnTapped());
        }

        private ICommand _tapCommand;

        public ICommand TapCommand => _tapCommand;

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <returns></returns>
        async Task OnTapped()
        {
            isInit = true;
            await _contentNavigationService.PopAsync();
        }

        public static bool isInit = false;

        private IContentNavigationService _contentNavigationService;

        private ImageSource _imageSource;

        /// <summary>
        /// 图片源
        /// </summary>
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => Set(nameof(ImageSource), ref _imageSource, value);

        }

    }
}
