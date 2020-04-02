using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BowlingGame.Tests
{
    public class Tests
    {
        [Test]
        public void ShouldReturnTheScoreOfAFrame()
        {
            var ab = new Ab();

            var result = ab.SubmitScore(1, 1, 4);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void ShouldReturnTheCumulativeScore()
        {
            var ab = new Ab();
            ab.SubmitScore(1, 1, 5);
            ab.SubmitScore(2, 4, 5);
            ab.SubmitScore(3, 3, 5);

            Assert.That(ab.TotalScore, Is.EqualTo(23));
        }

        [Test]
        public void ShouldBeAbleToReterieveFrameSpecificTotal()
        {
            var ab = new Ab();
            ab.SubmitScore(1, 1, 5);
            ab.SubmitScore(2, 4, 5);
            ab.SubmitScore(3, 3, 5);

            Assert.That(ab.FrameScore[2], Is.EqualTo(9));
        }

        [Test]
        public void ShouldNotThrowExceptionForBonusCalculationForTheFirstFrame()
        {
            var ab = new Ab();
            Assert.DoesNotThrow(() => ab.SubmitScore(1, 1, 5));

        }

        [Test]
        public void ShouldAddBonusToPreviousFrameWhenItIsSpare()
        {
            var ab = new Ab();
            ab.SubmitScore(1, 1, 5);
            ab.SubmitScore(2, 5, 5);
            ab.SubmitScore(3, 3, 5);

            Assert.That(ab.FrameScore[2], Is.EqualTo(13));
        }
    }

    internal class Ab
    {
        public Ab()
        {
            FrameScore = new Dictionary<int, int>();
        }

        public int TotalScore { get; internal set; }
        public Dictionary<int, int> FrameScore { get; internal set; }

        internal object SubmitScore(int frameNumber, int v1, int v2)
        {
            if (FrameScore.ContainsKey(frameNumber - 1) && FrameScore[frameNumber - 1] == 10)
            {
                FrameScore[frameNumber - 1] += v1;
            }
            int frameScore = v1 + v2;
            FrameScore[frameNumber] = frameScore;
            TotalScore += frameScore;
            return frameScore;
        }
    }
}