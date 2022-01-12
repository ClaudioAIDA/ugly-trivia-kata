using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Trivia.Tests
{
    [UsesVerify]
    public class GameTest:VerifyBase
    {
        public GameTest():base()
        {
        }

        [Fact]
        public Task GoldenRecordTest()
        {
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));

            for (var i = 0; i < 1000; i++)
            {
                GameRunner.PlayGame(new Random(i));
            }

            return Verify(consoleOutput);
        }
    }
}