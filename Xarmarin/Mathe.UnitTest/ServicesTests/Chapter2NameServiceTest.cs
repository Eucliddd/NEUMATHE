using System.Threading.Tasks;
using Mathe.Services;
using NUnit.Framework;

namespace TestProject1.ServicesTests
{
    public class Chapter2NameServiceTest
    {
        [Test]
        public async Task TestChapter2NameService()
        {
            IChapter2NameService chapter2NameService = new Chapter2NameService();
            await chapter2NameService.InitializeAsync();
            Assert.AreEqual("7.4.2 复合函数中间变量为多元函数情形(43题)", chapter2NameService.Chapter2Name("7-4-2"));
            Assert.AreEqual("10.5.2  y”= f (x，y’)型(26题)", chapter2NameService.Chapter2Name("10-5-2"));
        }
    }
}