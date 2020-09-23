namespace BowlingGame
{
    public class ScoreDisplayer
    {
        private readonly BowlingScoreCalculator scoreCalculator_;

        public ScoreDisplayer()
        {
            scoreCalculator_ = new BowlingScoreCalculator();
        }

        public string DisplayScore(int pinCount)
        {
            string rollCount = string.Empty;
            string frameScore = string.Empty;
            scoreCalculator_.SubmitScore(pinCount);
            for (int i = 0; i < scoreCalculator_.Frames.Count; i++)
            {
                foreach (var roll in scoreCalculator_.Frames[i].Roll)
                {
                    rollCount += roll.ToString() + " ";
                }

                frameScore += scoreCalculator_.Frames[i].GetScore().ToString() + "  ";
            }

           

            var score = rollCount + "\n" + frameScore;
            if (scoreCalculator_.Frames.Count > 10)
            {
                score += "\nGame Over!";
            }

             System.Console.WriteLine(string.Format("Score:{0}", score));
            //System.Console.WriteLine("FrameScore:", frameScore);
            return score;
        }
    }
}