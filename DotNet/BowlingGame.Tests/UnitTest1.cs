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
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var ab = new Ab();

            var result = ab.SubmitScore(frame);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void ShouldReturnTheCumulativeScore()
        {
            var ab = new Ab();
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 4, Roll2 = 5 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            ab.SubmitScore(frame);
            ab.SubmitScore(frame2);
            ab.SubmitScore(frame3);

            Assert.That(ab.TotalScore, Is.EqualTo(22));
        }

        [Test]
        public void ShouldBeAbleToReterieveFrameSpecificTotal()
        {
            var ab = new Ab();
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 4, Roll2 = 5 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            ab.SubmitScore(frame);
            ab.SubmitScore(frame2);
            ab.SubmitScore(frame3);

            Assert.That(ab.Frames.Find(f => f.FrameNumber == 2).FrameScore, Is.EqualTo(9));
        }

        [Test]
        public void ShouldNotThrowExceptionForBonusCalculationForTheFirstFrame()
        {
            var ab = new Ab();
            var frame = new Frame { FrameNumber = 1, Roll1 = 3, Roll2 = 5 };
            Assert.DoesNotThrow(() => ab.SubmitScore(frame));

        }

        [Test]
        public void ShouldAddBonusToPreviousFrameWhenItIsSpareInPreviousFrame()
        {
            var ab = new Ab();
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 5, Roll2 = 5 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            ab.SubmitScore(frame);
            ab.SubmitScore(frame2);
            ab.SubmitScore(frame3);

            Assert.That(ab.Frames.Find(f => f.FrameNumber == 2).FrameScore, Is.EqualTo(13));
        }

        [Test]
        public void ShouldConsiderThirdRollIfThereIsASpareInLastFrame()
        {
            var ab = new Ab();
            for (int i = 1; i < 10; i++)
            {
                var frame = new Frame { FrameNumber = i, Roll1 = 3, Roll2 = 5 };

                ab.SubmitScore(frame);
            }

            var frame10 = new Frame { FrameNumber = 10, Roll1 = 4, Roll2 = 6, Roll3 = 4 };
            ab.SubmitScore(frame10);
            Assert.That(ab.Frames.Find(f => f.FrameNumber == 10).FrameScore, Is.EqualTo(14));

        }

        [Test]
        public void ShouldDisregardRoll2IfItIsAStrike()
        {
            var ab = new Ab();
            var frame10 = new Frame { FrameNumber = 1, Roll1 = 10, Roll2 = 6 };
            ab.SubmitScore(frame10);
            Assert.That(ab.Frames.Find(f => f.FrameNumber == 1).FrameScore, Is.EqualTo(10));

        }

        [Test]
        public void ShouldUpdateTheFrameIfItIsStrike()
        {
            var ab = new Ab();
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 10, Roll2 = 0 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            ab.SubmitScore(frame);
            ab.SubmitScore(frame2);
            ab.SubmitScore(frame3);

            Assert.That(frame2.IsStrike, Is.True);
        }
        [Test]
        public void ShouldAddBonusToForNextTwoConsecutiveRolesIfItIsAStrike()
        {
            var ab = new Ab();
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 10, Roll2 = 0 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            ab.SubmitScore(frame);
            ab.SubmitScore(frame2);
            ab.SubmitScore(frame3);

            Assert.That(ab.Frames.Find(f => f.FrameNumber == 2).FrameScore, Is.EqualTo(18));

        }

        [Test]
        public void ShouldAddBonusWhenThereAreTwoOrMoreConsecutiveStrikes()
        {
            var ab = new Ab();
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 10, Roll2 = 0 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 10, Roll2 = 0 };
            var frame4 = new Frame { FrameNumber = 4, Roll1 = 5, Roll2 = 5 };

            ab.SubmitScore(frame);
            ab.SubmitScore(frame2);
            ab.SubmitScore(frame3);
            ab.SubmitScore(frame4);

            Assert.That(ab.Frames.Find(f => f.FrameNumber == 2).FrameScore, Is.EqualTo(25));
            Assert.That(ab.Frames.Find(f => f.FrameNumber == 3).FrameScore, Is.EqualTo(20));

        }


    }

    internal class Ab
    {
        public Ab()
        {
           Frames = new List<Frame>();
        }

        public int TotalScore { get; internal set; }
        public List<Frame> Frames { get; internal set; }

       internal object SubmitScore(Frame frame)
        {
            int frameNumber = frame.FrameNumber;
            int roll1 = frame.Roll1;
            int roll2 = frame.Roll2;
            int frameScore = 0;

            if (roll1 == 10)
            {
                frame.IsStrike = true;
            }

            if (Frames.Exists((f) => f.FrameNumber == frame.FrameNumber - 1) && Frames.Find(f => f.FrameNumber == frameNumber - 1).FrameScore == 10)
            {
                Frames.Find(f => f.FrameNumber == frameNumber - 1).FrameScore += roll1;
                if (Frames.Find(f => f.FrameNumber == frameNumber - 1).IsStrike)
                {
                    if (Frames.Exists((f) => f.FrameNumber == frame.FrameNumber - 2) && Frames.Find(f => f.FrameNumber == frameNumber - 2).IsStrike)
                    {
                        Frames.Find(f => f.FrameNumber == frameNumber - 2).FrameScore += roll1;
                    }

                    Frames.Find(f => f.FrameNumber == frameNumber - 1).FrameScore += roll2;

                }
            }

            frameScore += roll1;
            if (roll1 != 10 || frameNumber == 10)
            {
                frameScore += roll2;
            }

            if (frameNumber == 10)
            {
                frameScore += frame.Roll3;
            }
            frame.FrameScore = frameScore;
            TotalScore += frameScore;
            Frames.Add(frame);
            return frameScore;

        }
    }

    public class Frame
    {
        public int FrameNumber { get; set; }
        public int Roll1 { get; set; }
        public int Roll2 { get; set; }
        public int Roll3 { get; set; }
        public bool IsStrike { get; internal set; }
        public int FrameScore { get; set; }
    }
}