using BowlingGame.Frame;

namespace BowlingGame.Game;



public interface IBowlingGame
{
    List<IFrameDto> GetFrames();
    int TotalScore { get; }
    void RollBowlingBall();
    bool IsGameCompleted();

}