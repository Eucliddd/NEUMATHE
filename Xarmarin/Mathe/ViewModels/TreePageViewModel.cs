using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mathe.Services;
using Syncfusion.XForms.TreeView;
using Xamarin.Forms;

namespace Mathe.ViewModels
{
    /// <summary>
    /// 树页面视图模型
    /// </summary>
    public class TreePageViewModel : ViewModelBase
    {
        /// <summary>
        /// 点击项命令
        /// </summary>
        public ICommand ItemCommand { get; private set; }


        public TreePageViewModel(IProblemStorage problemStorage, ITreeService treeService,
            IContentNavigationService contentNavigationService)
        {
            _problemStorage = problemStorage;
            _treeService = treeService;
            _contentNavigationService = contentNavigationService;
            ItemCommand = new Command<Syncfusion.XForms.TreeView.ItemTappedEventArgs>(TreeView_ItemTapped);
        }

        /// <summary>
        /// 树
        /// </summary>
        public ObservableCollection<TreeNode> Tree
        {
            get => _tree;
            set { Set(nameof(Tree), ref _tree, value); }
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="e"></param>
        private void TreeView_ItemTapped(Syncfusion.XForms.TreeView.ItemTappedEventArgs e)
        {
            _contentNavigationService.NavigateToAsync(IContentNavigationService.ContentNavigationConstants.ProblemPage,
                ((TreeNode) e.Node.Content).id);
            // Application.Current.MainPage.DisplayAlert("Item Tapped", "TreeView" + ((TreeNode)e.Node.Content).id + " item tapped", "Close");
        }


        private ObservableCollection<TreeNode> _tree;

        /// <summary>
        /// 初始化命令
        /// </summary>
        public RelayCommand InitCommand => new RelayCommand(async () => await InitCommandFunction());

        /// <summary>
        /// 初始化命令函数
        /// </summary>
        /// <returns></returns>
        public async Task InitCommandFunction()
        {
            await _problemStorage.InitializeAsync();
            await _treeService.InitializeAsync();
            Tree = _treeService.GetTree();
        }

        private IProblemStorage _problemStorage;
        private ITreeService _treeService;
        private IContentNavigationService _contentNavigationService;
    }
}