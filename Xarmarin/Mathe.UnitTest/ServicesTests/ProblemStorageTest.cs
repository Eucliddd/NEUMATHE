using System.Threading.Tasks;
using Mathe.Services;
using Moq;
using NUnit.Framework;
using TestProject1.ServicesTests;

namespace TestProject1.ServicesTests
{
    public class ProblemStorageTest
    {
        [Test]
        public async Task OnGetProblemAsyncTest()
        {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock
                .Setup(p => p.Get(ProblemStorageConstants.VersionKey, -1))
                .Returns(ProblemStorageConstants.Version);
            IProblemStorage problemStorage = Utils.GetMockStorage();

            await problemStorage.InitializeAsync();

            var prob = await problemStorage.GetProblemAsync(0);

            Assert.AreEqual("1-3-3", prob.chapter);

            await problemStorage.CloseAsync();
        }
    }
}