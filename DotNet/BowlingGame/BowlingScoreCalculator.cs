using System.Collections.Generic;

namespace BowlingGame
{
    public class BowlingScoreCalculator
    {
        public BowlingScoreCalculator()
        {
            Frame = new Frame();
            Frames = new List<Frame>();
            Frames.Add(Frame);
        }

        private Frame Frame { get; set; }
        public List<Frame> Frames { get; set; }

        public void SubmitScore(int pinCount)
        {
            Frame.Roll.Add(pinCount);
            AddSpareBonus();
            AddStrikeBonus();

            if ((IsStrike(Frame) || Frame.Roll.Count > 1) && !(Frames.Count == 10 && (IsStrike(Frame) || IsSpare(Frame))))
            {
                AddNewFrame();
            }

        }

        private bool IsStrike(Frame frame)
        {
            if (frame.Roll.Count > 0)
            {
                return (int)frame.Roll[0] == 10;
            }
            return false;
        }

        private bool IsSpare(Frame frame)
        {
            if (frame.Roll.Count > 1)
            {
                return ((int)frame.Roll[0] + (int)frame.Roll[1]) == 10;
            }
            return false;
        }

        private void AddSpareBonus()
        {
            if (Frames.Count > 1)
            {
                if (IsSpare(Frames[Frames.Count - 2]))
                {
                    Frames[Frames.Count - 2].Bonus = (int)Frames[Frames.Count - 1].Roll[0];
                }
            }
        }

        private void AddStrikeBonus()
        {
            if (Frames.Count > 1)
            {
                if (IsStrike(Frames[Frames.Count - 2]))
                {
                    Frames[Frames.Count - 2].Bonus = (int)Frames[Frames.Count - 1].Roll[0];
                    if (Frame.Roll.Count > 1)
                    {
                        Frames[Frames.Count - 2].Bonus += (int)Frames[Frames.Count - 1].Roll[1];
                    }
                }
            }
            if (Frames.Count > 2)
            {
                if (IsStrike(Frames[Frames.Count - 3]) && IsStrike(Frames[Frames.Count - 2]))
                {
                    Frames[Frames.Count - 3].Bonus = (int)Frames[Frames.Count - 1].Roll[0];
                    Frames[Frames.Count - 3].Bonus += (int)Frames[Frames.Count - 2].Roll[0];
                }
            }
        }

        private void AddNewFrame()
        {
            Frame = new Frame();
            Frames.Add(Frame);
        }
    }
}