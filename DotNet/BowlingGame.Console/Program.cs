using System;
using BowlingGame;

namespace BowlingGame.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World");
            var scoreDisplayer = new ScoreDisplayer();

            var score = string.Empty;

            while (!score.Contains("Game Over!"))
            {
                var pinCount = System.Console.ReadLine();
                score = scoreDisplayer.DisplayScore(Convert.ToInt32(pinCount));
                System.Console.WriteLine(score);

            }
        }
    }
}
