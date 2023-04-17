using BowlingGame.Frame;

namespace BowlingGame.ScoringStrategy;

public interface IScoringStrategy
{
    int ScoreFrame(IFrame frame);
}