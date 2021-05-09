using GalaSoft.MvvmLight.Ioc;
using Mathe.Services;
using Mathe.Views;

namespace Mathe.ViewModels
{
    public class ViewModelLocator
    {
        public TreePageViewModel TreePageViewModel => SimpleIoc.Default.GetInstance<TreePageViewModel>();

        public ProblemPageViewModel ProblemPageViewModel => SimpleIoc.Default.GetInstance<ProblemPageViewModel>();

        public ZoomPageViewModel ZoomPageViewModel => SimpleIoc.Default.GetInstance<ZoomPageViewModel>();

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<IPreferenceStorage, PreferenceStorage>();
            SimpleIoc.Default.Register<IProblemStorage, ProblemStorage>();
            SimpleIoc.Default.Register<TreePageViewModel>();
            SimpleIoc.Default.Register<ProblemPageViewModel>();
            SimpleIoc.Default.Register<ZoomPageViewModel>();
            SimpleIoc.Default.Register<ITreeService, TreeService>();
            SimpleIoc.Default.Register<IContentNavigationService, ContentNavigationService>();
            SimpleIoc.Default.Register<IContentPageActivationService, ContentPageActivationService>();
            SimpleIoc.Default.Register<IChapter2NameService, Chapter2NameService>();
            SimpleIoc.Default.Register<Utils>();
        }
    }
}