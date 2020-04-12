using NUnit.Framework;

namespace BowlingGame.Tests
{
    public class BowlingScoreCalculatorTests
    {

        [Test]
        public void ShouldStoreFirstRollInFrame()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            scoreCalculator.SubmitScore(2);
            scoreCalculator.SubmitScore(4);

            Assert.That(scoreCalculator.Frames[0].Roll[0], Is.EqualTo(2));
        }

        [Test]
        public void ShouldStoreSecondRollInFrame()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            scoreCalculator.SubmitScore(2);
            scoreCalculator.SubmitScore(4);

            Assert.That(scoreCalculator.Frames[0].Roll[1], Is.EqualTo(4));
        }

        [Test]
        public void ShouldCreateNewFrameAfterTwoRolls()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            scoreCalculator.SubmitScore(2);
            scoreCalculator.SubmitScore(4);
            var frame1 = scoreCalculator.Frames[0];
            scoreCalculator.SubmitScore(3);
            scoreCalculator.SubmitScore(2);

            Assert.That(scoreCalculator.Frames[1], Is.Not.SameAs(frame1));
        }

        [Test]
        public void ShouldchangeTheFrameIfItIsAStrike()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            scoreCalculator.SubmitScore(2);
            scoreCalculator.SubmitScore(4);
            var frame1 = scoreCalculator.Frames[0];
            scoreCalculator.SubmitScore(10);
            var frame2 = scoreCalculator.Frames[1];

            scoreCalculator.SubmitScore(3);
            var frame3 = scoreCalculator.Frames[2];
            scoreCalculator.SubmitScore(2);

            Assert.That(frame2, Is.Not.SameAs(frame3));
        }

        [Test]
        public void ShouldNotCreateANewFrameIfItIsStrikeInTenthFrame()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            for (int i = 0; i < 9; i++)
            {
                scoreCalculator.SubmitScore(2);
                scoreCalculator.SubmitScore(4);
            }

            scoreCalculator.SubmitScore(10);
            scoreCalculator.SubmitScore(4);

            Assert.That(scoreCalculator.Frames[9].Roll[1], Is.EqualTo(4));
        }

        [Test]
        public void ShouldAddThirdRollIfItIsStrikeInTenthFrame()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            for (int i = 0; i < 9; i++)
            {
                scoreCalculator.SubmitScore(2);
                scoreCalculator.SubmitScore(4);
            }

            scoreCalculator.SubmitScore(10);
            scoreCalculator.SubmitScore(4);
            scoreCalculator.SubmitScore(5);

            Assert.That(scoreCalculator.Frames[9].Roll[2], Is.EqualTo(5));
        }

        [Test]
        public void ShouldAddThirdRollIfItIsSpareInTenthFrame()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            for (int i = 0; i < 9; i++)
            {
                scoreCalculator.SubmitScore(2);
                scoreCalculator.SubmitScore(4);
            }

            scoreCalculator.SubmitScore(5);
            scoreCalculator.SubmitScore(5);
            scoreCalculator.SubmitScore(10);

            Assert.That(scoreCalculator.Frames[9].Roll[2], Is.EqualTo(10));
        }

        [Test]
        public void ShouldAddBonusWhenItIsSpare()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            scoreCalculator.SubmitScore(5);
            scoreCalculator.SubmitScore(5);

            scoreCalculator.SubmitScore(3);
            scoreCalculator.SubmitScore(5);

            Assert.That(scoreCalculator.Frames[0].Bonus, Is.EqualTo(3));
        }

        [Test]
        public void ShouldAddFirstRollToBonusWhenItIsStrike()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            scoreCalculator.SubmitScore(5);
            scoreCalculator.SubmitScore(5);

            scoreCalculator.SubmitScore(10);

            scoreCalculator.SubmitScore(3);
            //scoreCalculator.SubmitScore(5);

            Assert.That(scoreCalculator.Frames[1].Bonus, Is.EqualTo(3));
        }


        [Test]
        public void ShouldAddSecondRollToBonusWhenItIsStrike()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            scoreCalculator.SubmitScore(5);
            scoreCalculator.SubmitScore(5);

            scoreCalculator.SubmitScore(10);

            scoreCalculator.SubmitScore(3);
            scoreCalculator.SubmitScore(5);

            Assert.That(scoreCalculator.Frames[1].Bonus, Is.EqualTo(8));
        }
        [Test]
        public void ShouldAddFirstRollOfNextFrameAsWellToBonusWhenTwoConsecutiveStrikes()
        {
            var scoreCalculator = new BowlingScoreCalculator();
            scoreCalculator.SubmitScore(5);
            scoreCalculator.SubmitScore(5);

            scoreCalculator.SubmitScore(10);

            scoreCalculator.SubmitScore(10);

            scoreCalculator.SubmitScore(5);

            Assert.That(scoreCalculator.Frames[1].Bonus, Is.EqualTo(15));
        }
    }
}