namespace BowlingGame.Frame;


public class FrameDto : IFrameDto
{
    public int? FirstRoll { get; private set; }
    public int? SecondRoll { get; }
    public int? ThirdRoll { get; }
    public int? FrameScore { get; }
    public int? TotalScore { get;  }
    
    public bool IsOpenFrame { get; }

    public FrameDto(int? firstRoll=null, int? secondRoll=null, int? thirdRoll=null, int? frameScore=null, int? totalScore=null, bool isOpenFrame=false)
    {
        FirstRoll = firstRoll;
        SecondRoll = secondRoll;
        ThirdRoll = thirdRoll;
        FrameScore = frameScore;
        TotalScore = totalScore;
        IsOpenFrame = isOpenFrame;
    }
}