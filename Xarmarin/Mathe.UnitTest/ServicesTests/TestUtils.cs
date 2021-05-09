using Mathe.Services;
using Moq;

namespace TestProject1.ServicesTests
{
    public class Utils
    {
        public static IProblemStorage GetMockStorage()
        {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock
                .Setup(p => p.Get(ProblemStorageConstants.VersionKey, -1))
                .Returns(ProblemStorageConstants.Version);
            IProblemStorage problemStorage = new ProblemStorage(preferenceStorageMock.Object);
            return problemStorage;
        }
    }
}