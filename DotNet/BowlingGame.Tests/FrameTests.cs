using NUnit.Framework;

namespace BowlingGame.Tests
{
    public class FrameTests
    {
        [Test]
        public void ShouldReturnTheTotalScoreOfAFrame()
        {
            var frame = new Frame();
            frame.Roll.Add(5);
            frame.Roll.Add(5);
            frame.Bonus=3;
            frame.Bonus+=4;

            Assert.That(frame.GetScore(),Is.EqualTo(17));
        }
    }
}