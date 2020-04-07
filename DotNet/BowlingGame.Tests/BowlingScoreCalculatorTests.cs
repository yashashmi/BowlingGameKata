using System;
using NUnit.Framework;
using BowlingGame;

namespace BowlingGame.Tests
{
    public class BowlingScoreCalculatorTests
    {
        BowlingGame.BowlingScoreCalculator scoreCalculator_;
        [SetUp]
        public void Setup()
        {
            scoreCalculator_ = new BowlingGame.BowlingScoreCalculator();
        }


        [Test]
        public void ShouldReturnTheScoreOfAFrame()
        {
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            
            var result = scoreCalculator_.SubmitScore(frame);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void ShouldReturnTheCumulativeScore()
        {
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 4, Roll2 = 5 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            scoreCalculator_.SubmitScore(frame);
            scoreCalculator_.SubmitScore(frame2);
            scoreCalculator_.SubmitScore(frame3);

            Assert.That(scoreCalculator_.TotalScore, Is.EqualTo(22));
        }

        [Test]
        public void ShouldBeAbleToReterieveFrameSpecificTotal()
        {
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 4, Roll2 = 5 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            scoreCalculator_.SubmitScore(frame);
            scoreCalculator_.SubmitScore(frame2);
            scoreCalculator_.SubmitScore(frame3);

            Assert.That(scoreCalculator_.Frames.Find(f => f.FrameNumber == 2).FrameScore, Is.EqualTo(9));
        }

        [Test]
        public void ShouldNotThrowExceptionForBonusCalculationForTheFirstFrame()
        {
            var frame = new Frame { FrameNumber = 1, Roll1 = 3, Roll2 = 5 };
            Assert.DoesNotThrow(() => scoreCalculator_.SubmitScore(frame));

        }

        [Test]
        public void ShouldAddBonusToPreviousFrameWhenItIsSpareInPreviousFrame()
        {
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 5, Roll2 = 5 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            scoreCalculator_.SubmitScore(frame);
            scoreCalculator_.SubmitScore(frame2);
            scoreCalculator_.SubmitScore(frame3);

            Assert.That(scoreCalculator_.Frames.Find(f => f.FrameNumber == 2).FrameScore, Is.EqualTo(13));
        }

        [Test]
        public void ShouldConsiderThirdRollIfThereIsASpareInLastFrame()
        {
            for (int i = 1; i < 10; i++)
            {
                var frame = new Frame { FrameNumber = i, Roll1 = 3, Roll2 = 5 };

                scoreCalculator_.SubmitScore(frame);
            }

            var frame10 = new Frame { FrameNumber = 10, Roll1 = 4, Roll2 = 6, Roll3 = 4 };
            scoreCalculator_.SubmitScore(frame10);
            Assert.That(scoreCalculator_.Frames.Find(f => f.FrameNumber == 10).FrameScore, Is.EqualTo(14));

        }

        [Test]
        public void ShouldDisregardRoll2IfItIsAStrike()
        {
           var frame10 = new Frame { FrameNumber = 1, Roll1 = 10, Roll2 = 6 };
            scoreCalculator_.SubmitScore(frame10);
            Assert.That(scoreCalculator_.Frames.Find(f => f.FrameNumber == 1).FrameScore, Is.EqualTo(10));

        }

        [Test]
        public void ShouldUpdateTheFrameIfItIsStrike()
        {
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 10, Roll2 = 0 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            scoreCalculator_.SubmitScore(frame);
            scoreCalculator_.SubmitScore(frame2);
            scoreCalculator_.SubmitScore(frame3);

            Assert.That(frame2.IsStrike, Is.True);
        }
        [Test]
        public void ShouldAddBonusToForNextTwoConsecutiveRolesIfItIsAStrike()
        {
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 10, Roll2 = 0 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 3, Roll2 = 5 };
            scoreCalculator_.SubmitScore(frame);
            scoreCalculator_.SubmitScore(frame2);
            scoreCalculator_.SubmitScore(frame3);

            Assert.That(scoreCalculator_.Frames.Find(f => f.FrameNumber == 2).FrameScore, Is.EqualTo(18));

        }

        [Test]
        public void ShouldAddBonusWhenThereAreTwoOrMoreConsecutiveStrikes()
        {
            var frame = new Frame { FrameNumber = 1, Roll1 = 1, Roll2 = 4 };
            var frame2 = new Frame { FrameNumber = 2, Roll1 = 10, Roll2 = 0 };
            var frame3 = new Frame { FrameNumber = 3, Roll1 = 10, Roll2 = 0 };
            var frame4 = new Frame { FrameNumber = 4, Roll1 = 5, Roll2 = 5 };

            scoreCalculator_.SubmitScore(frame);
            scoreCalculator_.SubmitScore(frame2);
            scoreCalculator_.SubmitScore(frame3);
            scoreCalculator_.SubmitScore(frame4);

            Assert.That(scoreCalculator_.Frames.Find(f => f.FrameNumber == 2).FrameScore, Is.EqualTo(25));
            Assert.That(scoreCalculator_.Frames.Find(f => f.FrameNumber == 3).FrameScore, Is.EqualTo(20));

        }


    }
}