using System;
using NUnit.Framework;

namespace BowlingGame.Tests
{
    public class ScoreBoardDisplayTests
    {
        [Test]
        public void ShouldReturnScoreOfMultiFrames()
        {
            var ab = new ScoreDisplayer();


            ab.DisplayScore(1);
            ab.DisplayScore(3);
            ab.DisplayScore(5);
            ab.DisplayScore(5);
            ab.DisplayScore(4);
            ab.DisplayScore(3);
            var result = ab.DisplayScore(1);

            Assert.That(result, Is.EqualTo("1\t3\t5\t5\t4\t3\t\n4\t\t14\t\t7\t\t"));
        }

        [Test]
        public void ShouldReturnGameOverMessageWhenAllFramesDone()
        {
            var ab = new ScoreDisplayer();
            string result = string.Empty;
            for (int i = 0; i < 22; i++)
            {
                result = ab.DisplayScore(5);
            }

            Assert.That(result, Is.EqualTo("5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t5\t\n15\t\t15\t\t15\t\t15\t\t15\t\t15\t\t15\t\t15\t\t15\t\t15\t\t\nGame Over!"));
        }
    }
}