using System.Collections.Generic;
using System.Threading.Tasks;
using Mathe.Models;
using NUnit.Framework;

namespace TestProject1.ServicesTests
{
    public class UtilsTest
    {
        [Test]
        public async Task TestProblemDisplayToProblem()
        {
            var storage = Utils.GetMockStorage();
            await storage.InitializeAsync();
            var problem = await storage.GetProblemAsync(0);
            var cs = new Mathe.Services.Chapter2NameService();
            await cs.InitializeAsync();
            var utils = new Mathe.Services.Utils(cs);
            var prob = utils.ProblemsToDisplay(new List<Problem> {problem})[0];
            var turn = utils.ProblemDisplayToProblem(prob);
            var tmp = turn.mark;
            turn.mark = 1;
            await storage.UpdateProblemsAsync(turn);
            var newProb = await storage.GetProblemAsync(0);
            Assert.AreEqual(1, newProb.mark);
            turn.mark = tmp;
            await storage.UpdateProblemsAsync(turn);
            newProb = await storage.GetProblemAsync(0);
            Assert.AreEqual(tmp, newProb.mark);
        }
    }
}