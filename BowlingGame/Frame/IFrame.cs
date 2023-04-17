namespace BowlingGame.Frame;


public interface IFrame
{
    IFrame? Next { set; get; }
    IFrame? Previous { set; get; }
    List<int?> Rolls { get; }
    int? FrameScore { get; }
    int? TotalScore { get; }
    FrameType Type { get; }
    
    bool IsOpenFrame { get; }

    IFrameDto ToFrameDto();

    void AddRoll(int pinsKnockedDown);
}
