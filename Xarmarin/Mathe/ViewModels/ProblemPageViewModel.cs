using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mathe.Models;
using Mathe.Services;
using Mathe.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;


namespace Mathe.ViewModels
{
    /// <summary>
    /// 问题页视图模型
    /// </summary>
    public class ProblemPageViewModel : ViewModelBase
    {
        public ProblemPageViewModel(IProblemStorage problemStorage, Utils utils,
            IContentNavigationService contentNavigationService,
            IChapter2NameService chapter2NameService)
        {
            _problemStorage = problemStorage;
            _contentNavigationService = contentNavigationService;
            _utils = utils;
            _tapCommand = new Command(OnTapped);
            _doubleTapCommand = new Command(OnDoubleTapped);
            _chapter2NameService = chapter2NameService;
            _settingCommand = new Command(OnClickedSetting);
        }


        private IContentNavigationService _contentNavigationService;

        private Utils _utils;

        private IProblemStorage _problemStorage;

        private IChapter2NameService _chapter2NameService;

        /// <summary>
        /// 章节名
        /// </summary>
        private string _chapterName = "";

        /// <summary>
        /// 章节名
        /// </summary>
        public string ChapterName
        {
            get => _chapterName;
            set => Set(nameof(ChapterName), ref _chapterName, value);
        }

        /// <summary>
        /// 是否标记
        /// </summary>
        private string _markName = "";

        /// <summary>
        /// 是否标记
        /// </summary>
        public string MarkName
        {
            get => _markName;
            set => Set(nameof(MarkName), ref _markName, value);
        }

        /// <summary>
        /// 是否完成
        /// </summary>
        private string _doneName = "";

        /// <summary>
        /// 是否完成
        /// </summary>
        public string DoneName
        {
            get => _doneName;
            set => Set(nameof(DoneName), ref _doneName, value);
        }

        /// <summary>
        /// 初始化命令
        /// </summary>
        public RelayCommand InitCommand => new RelayCommand(async () => await InitFunction());

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <returns></returns>
        public async Task InitFunction()
        {
            if (ZoomPageViewModel.isInit)
            {
                ZoomPageViewModel.isInit = false;
                return;
            }
            await _problemStorage.InitializeAsync();
            await _chapter2NameService.InitializeAsync();
            ChapterName = _chapter2NameService.Chapter2Name(Chapter);

            Problems = _utils.ProblemsToDisplay(
                await _problemStorage.GetProblemsByParentAsync(Chapter));


            if (CurrentItem.img0 != null && Problems.Count > 0)
            {
                CurrentItem = Problems[0];
            }

            CalculateCurrentPercent();

        }



        /// <summary>
        /// 标记命令
        /// </summary>
        public RelayCommand MarkCommand => new RelayCommand(async () => await MarkFunction());

        /// <summary>
        /// 标记函数
        /// </summary>
        /// <returns></returns>
        public async Task MarkFunction()
        {
            if (Problems.Count != 0)
            {
                CurrentItem.mark = (CurrentItem.mark & 1) ^ 1;
                await _problemStorage.UpdateProblemsAsync(_utils.ProblemDisplayToProblem(CurrentItem));
                CalculateCurrentPercent();
            }
        }

        /// <summary>
        /// 完成命令
        /// </summary>
        public RelayCommand DoneCommand => new RelayCommand(async () => await DoneFunction());

        /// <summary>
        /// 完成函数
        /// </summary>
        /// <returns></returns>
        public async Task DoneFunction()
        {
            if (Problems.Count !=0)
            {
                CurrentItem.done = (CurrentItem.done & 1) ^ 1;
                await _problemStorage.UpdateProblemsAsync(_utils.ProblemDisplayToProblem(CurrentItem));
                CalculateCurrentPercent();
            }
        }

        /// <summary>
        /// 标记展示命令
        /// </summary>

        private ICommand _settingCommand;

        /// <summary>
        /// 标记展示命令
        /// </summary>
        public ICommand SettingCommand => _settingCommand;

        /// <summary>
        /// 点击标记事件
        /// </summary>
        public async void OnClickedSetting()
        {
            string result = await Application.Current.MainPage.DisplayActionSheet("设置显示的内容", "取消", null, "全部",
                "标记题√做完题×", "标记题×做完题√", "标记题√做完题√", "标记题×做完题×");

            switch (result)
            {
                case "全部":
                    Problems = _utils.ProblemsToDisplay(
                        await _problemStorage.GetProblemsByParentAsync(Chapter));
                    if (Problems.Count > 0)
                        CurrentItem = Problems[0];
                    CalculateCurrentPercent();
                    break;
                case "标记题√做完题×":
                    Problems = _utils.ProblemsToDisplay(
                        await _problemStorage.GetProblemsByParentAsync(Chapter, 1, 0));
                    if (Problems.Count > 0)
                        CurrentItem = Problems[0];
                    CalculateCurrentPercent();
                    break;
                case "标记题×做完题√":
                    Problems = _utils.ProblemsToDisplay(
                        await _problemStorage.GetProblemsByParentAsync(Chapter, 0, 1));
                    if (Problems.Count > 0)
                        CurrentItem = Problems[0];
                    CalculateCurrentPercent();
                    break;
                case "标记题√做完题√":
                    Problems = _utils.ProblemsToDisplay(
                        await _problemStorage.GetProblemsByParentAsync(Chapter, 1, 1));
                    if (Problems.Count > 0)
                        CurrentItem = Problems[0];
                    CalculateCurrentPercent();
                    break;
                case "标记题×做完题×":
                    Problems = _utils.ProblemsToDisplay(
                        await _problemStorage.GetProblemsByParentAsync(Chapter, 0, 0));
                    if (Problems.Count > 0)
                        CurrentItem = Problems[0];
                    CalculateCurrentPercent();
                    break;
            }
        }


        /// <summary>
        /// 标记颜色
        /// </summary>
        private string _markColor = "";

        /// <summary>
        /// 标记颜色
        /// </summary>
        public string MarkColor
        {
            get => _markColor;
            set => Set(nameof(MarkColor), ref _markColor, value);
        }

        /// <summary>
        /// 完成颜色
        /// </summary>
        private string _doneColor = "";

        /// <summary>
        /// 完成颜色
        /// </summary>
        public string DoneColor
        {
            get => _doneColor;
            set => Set(nameof(DoneColor), ref _doneColor, value);
        }

        /// <summary>
        /// 展示答案命令
        /// </summary>
        public RelayCommand DisplayAnswerCommand => new RelayCommand(DisplayAnswerFunction);

        /// <summary>
        /// 展示答案函数
        /// </summary>
        public void DisplayAnswerFunction()
        {
            if (CurrentItem == null) return;
            _contentNavigationService.NavigateToAsync(IContentNavigationService.ContentNavigationConstants.ZoomPage,
                CurrentItem.imgans);
        }


        /// <summary>
        /// 当前项
        /// </summary>
        private ProblemDisplay _currentItem = new ProblemDisplay();

        /// <summary>
        /// 当前项
        /// </summary>
        public ProblemDisplay CurrentItem
        {
            get => _currentItem;
            set => Set(nameof(CurrentItem), ref _currentItem, value);
        }

        /// <summary>
        /// 当前位置
        /// </summary>
        private int _currentPosition = 0;

        /// <summary>
        /// 当前位置
        /// </summary>
        public int CurrentPosition
        {
            get => _currentPosition;
            set =>
                Set(nameof(CurrentPosition), ref _currentPosition, value);
        }

        /// <summary>
        /// 当前百分比
        /// </summary>
        private string _currentPercent = "0/0";

        /// <summary>
        /// 当前百分比
        /// </summary>
        public string CurrentPercent
        {
            get => _currentPercent;
            set => Set(nameof(CurrentPercent), ref _currentPercent, value);
        }

        /// <summary>
        /// 位置改变命令
        /// </summary>
        public RelayCommand PositionChangedCommand => new RelayCommand(CalculateCurrentPercent);

        /// <summary>
        /// 计算当前百分比
        /// </summary>
        public void CalculateCurrentPercent()
        {
            CurrentPercent = Problems.Count == 0 ? "0/0" : CurrentPosition + 1 + "/" + Problems.Count;
            if (Problems.Count !=0)
            {
                if (CurrentItem.mark == 1)
                    MarkColor = "Red";
                else
                    MarkColor = "Black";

                if (CurrentItem.done == 1)
                    DoneColor = "Green";
                else
                    DoneColor = "Black";
            }
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        private ICommand _tapCommand;

        /// <summary>
        /// 点击事件
        /// </summary>
        public ICommand TapCommand => _tapCommand;

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="arg">参数</param>
        void OnTapped(object arg)
        {
            _contentNavigationService.NavigateToAsync(IContentNavigationService.ContentNavigationConstants.ZoomPage,
                arg);
        }

        /// <summary>
        /// 双击命令
        /// </summary>
        private ICommand _doubleTapCommand;

        /// <summary>
        /// 双击命令
        /// </summary>
        public ICommand DoubleTapCommand => _doubleTapCommand;

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="arg"></param>
        void OnDoubleTapped(object arg)
        {
            var chosen = int.Parse((string) arg);
            if (chosen == CurrentItem.answer)
            {
                Application.Current.MainPage.DisplayAlert("Result", "Your answer is correct!", "Close");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Result", "Your answer is wrong!", "Close");
            }
        }

        /// <summary>
        /// 章节ID
        /// </summary>
        private string _chapter;

        /// <summary>
        /// 章节ID
        /// </summary>
        public string Chapter
        {
            get => _chapter;
            set { Set(nameof(Chapter), ref _chapter, value); }
        }

        /// <summary>
        /// 问题集
        /// </summary>
        private IList<ProblemDisplay> _problems = new List<ProblemDisplay>();

        /// <summary>
        /// 问题集
        /// </summary>
        public IList<ProblemDisplay> Problems
        {
            get => _problems;
            set => Set(nameof(Problems), ref _problems, value);
        }
    }




    /// <summary>
    /// 问题展示
    /// </summary>
    public class ProblemDisplay
    {
        /// <summary>
        /// 问题ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public ImageSource img0 { get; set; }

        /// <summary>
        /// 选项A
        /// </summary>
        public ImageSource img1 { get; set; }

        /// <summary>
        /// 选项B
        /// </summary>
        public ImageSource img2 { get; set; }

        /// <summary>
        /// 选项C
        /// </summary>
        public ImageSource img3 { get; set; }

        /// <summary>
        /// 选项D
        /// </summary>
        public ImageSource img4 { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public ImageSource imgans { get; set; }

        /// <summary>
        /// 问题内存流
        /// </summary>
        public MemoryStream ImgMemoryStream0;

        /// <summary>
        /// 选项A内存流
        /// </summary>
        public MemoryStream ImgMemoryStream1;

        /// <summary>
        /// 选项B内存流
        /// </summary>
        public MemoryStream ImgMemoryStream2;

        /// <summary>
        /// 选项C内存流
        /// </summary>
        public MemoryStream ImgMemoryStream3;

        /// <summary>
        /// 选项D内存流
        /// </summary>
        public MemoryStream ImgMemoryStream4;

        /// <summary>
        /// 答案内存流
        /// </summary>
        public MemoryStream ImgMemoryStreamAns;


        /// <summary>
        /// 答案
        /// </summary>
        public int answer { get; set; }

        /// <summary>
        /// 章节ID
        /// </summary>
        public string chapter { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public int done { get; set; }

        /// <summary>
        /// 是否标记
        /// </summary>
        public int mark { get; set; }
    }
}