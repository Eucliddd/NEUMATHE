using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mathe.Models;
using Mathe.ViewModels;
using ImageSource = Xamarin.Forms.ImageSource;

namespace Mathe.Services
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
        public Utils(IChapter2NameService chapter2NameService)
        {
            chapter2NameService.InitializeAsync();
            _chapter2NameService = chapter2NameService;
        }

        private IChapter2NameService _chapter2NameService;

        /// <summary>
        /// 问题数据集转数据展示
        /// </summary>
        /// <param name="problems"></param>
        /// <returns></returns>
        public IList<ProblemDisplay> ProblemsToDisplay(IList<Problem> problems)
        {
            return problems.Select(problem => new ProblemDisplay
                {
                    id = problem.id,
                    answer = problem.answer,
                    chapter = problem.chapter,
                    done = problem.done,
                    mark = problem.mark,
                    img0 = ImageSource.FromStream(() => new MemoryStream(problem.img0)),
                    img1 = ImageSource.FromStream(() => new MemoryStream(problem.img1)),
                    img2 = ImageSource.FromStream(() => new MemoryStream(problem.img2)),
                    img3 = ImageSource.FromStream(() => new MemoryStream(problem.img3)),
                    img4 = ImageSource.FromStream(() => new MemoryStream(problem.img4)),
                    imgans = ImageSource.FromStream(() => new MemoryStream(problem.imgans)),

                    ImgMemoryStream0 = new MemoryStream(problem.img0),
                    ImgMemoryStream1 = new MemoryStream(problem.img1),
                    ImgMemoryStream2 = new MemoryStream(problem.img2),
                    ImgMemoryStream3 = new MemoryStream(problem.img3),
                    ImgMemoryStream4 = new MemoryStream(problem.img4),
                    ImgMemoryStreamAns = new MemoryStream(problem.imgans)
                })
                .ToList();
        }

        /// <summary>
        /// 数据展示转数据
        /// </summary>
        /// <param name="problemDisplay"></param>
        /// <returns></returns>
        public Problem ProblemDisplayToProblem(ProblemDisplay problemDisplay)
        {
            return new Problem
            {
                id = problemDisplay.id,
                answer = problemDisplay.answer,
                chapter = problemDisplay.chapter,
                mark = problemDisplay.mark,
                done = problemDisplay.done,
                img0 = problemDisplay.ImgMemoryStream0.ToArray(),
                img1 = problemDisplay.ImgMemoryStream1.ToArray(),
                img2 = problemDisplay.ImgMemoryStream2.ToArray(),
                img3 = problemDisplay.ImgMemoryStream3.ToArray(),
                img4 = problemDisplay.ImgMemoryStream4.ToArray(),
                imgans = problemDisplay.ImgMemoryStreamAns.ToArray()
            };
        }
    }
}