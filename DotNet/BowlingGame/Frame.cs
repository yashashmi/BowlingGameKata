using System.Collections;

namespace BowlingGame
{
    public class Frame
    {
        public Frame()
        {
            Roll = new ArrayList();
        }
        public ArrayList Roll { get; internal set; }
        public int Bonus { get; internal set; }

        // public int GetScore()
        // {
        //     var score = 0;
        //     foreach (var roll in Roll)
        //     {
        //         score += (int)roll;
        //     }
        //     return score;
        // }

    }
}