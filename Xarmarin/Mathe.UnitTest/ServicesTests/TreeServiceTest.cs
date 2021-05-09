using System.Threading.Tasks;
using Mathe.Services;
using NUnit.Framework;

namespace TestProject1.ServicesTests
{
    public class TreeServiceTest
    {
        [Test]
        public async Task TestTreeService()
        {
            ITreeService treeService = new TreeService(null);
            await treeService.InitializeAsync();
            var tree = treeService.GetTree();
            Assert.AreEqual("C-1", tree[0].id);
            Assert.AreEqual("第1章 极限与连续(523题)", tree[0].text);
            Assert.AreEqual("1-1", tree[0].children[0].id);
        }
    }
}