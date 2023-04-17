namespace BowlingGame.Frame;

public interface IFrameDto
{
    int? FirstRoll { get; }
    int? SecondRoll { get; }
    int? ThirdRoll { get; }
    int? FrameScore { get; }
    int? TotalScore { get; }
    
    bool IsOpenFrame { get; }
}