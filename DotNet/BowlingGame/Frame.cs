using System;
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
        public int Bonus { get; set; }

        public object GetScore()
        {
            var score = 0;
            foreach (var roll in Roll)
            {
                score += (int)roll;
            }
            score += Bonus;
            return score;
        }

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