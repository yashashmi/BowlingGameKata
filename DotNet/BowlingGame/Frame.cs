namespace BowlingGame
{
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