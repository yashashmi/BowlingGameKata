using System.Collections.Generic;

namespace BowlingGame
{
    public class BowlingScoreCalculator
    {
        public BowlingScoreCalculator()
        {
            Frames = new List<Frame>();
        }

        public int TotalScore { get; internal set; }
        public List<Frame> Frames { get; internal set; }

        public int SubmitScore(Frame frame)
        {
            int frameNumber = frame.FrameNumber;
            int roll1 = frame.Roll1;
            int roll2 = frame.Roll2;
            int frameScore = 0;

            MarkStrike(roll1, frame);
            CalculateBonusForStrikeAndSpare(frame, frameNumber, roll1, roll2);

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

        private void CalculateBonusForStrikeAndSpare(Frame frame, int frameNumber, int roll1, int roll2)
        {
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
        }

        private void MarkStrike(int roll, Frame frame)
        {

            if (roll == 10)
            {
                frame.IsStrike = true;
            }
        }
    }
}