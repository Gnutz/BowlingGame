using BowlingGame.Frame;

namespace BowlingGame.ScoringStrategy;

public class WorldBowlingScoringStrategy : IScoringStrategy
{
    public int ScoreFrame(IFrame frame)
    {
        switch (frame.Type)
        {
            case FrameType.Strike:
                return 30;

            case FrameType.Spare:
                return 10 + (frame.Rolls.ElementAtOrDefault(0) ?? 0) ;
            default:
                //OpenFrame and NonMarked
                return frame.Rolls.Sum(x => x ?? 0);
        }
    }
}