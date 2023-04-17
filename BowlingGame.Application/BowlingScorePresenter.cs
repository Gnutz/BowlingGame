using BowlingGame.Frame;

namespace BowlingGame.Application;

public class BowlingScorePresenter
{
    public void DisplayGame(List<IFrameDto> frames)
    {
        var framesPlayed = frames.Count;
        var seriesView = "";
        for (var i = 0; i < frames.Count; i++)
        {
            var frame = frames[i];
            var isLastFrame = i == frames.Count - 1;
            var frameView = GetFrameString(frames[i], frame.IsOpenFrame, isLastFrame);
            seriesView += frameView;
            if (!isLastFrame)
            {
                seriesView += ", ";
            }
        }
        
        Console.WriteLine(seriesView);
    }

    string GetFrameString(IFrameDto frame, bool isCurrentFrame = false, bool isLastFrame = false)
    {
        var frameView = GetRollsPart(frame, isCurrentFrame, isLastFrame);

        frameView += $" => {frame.FrameScore}({frame.TotalScore})";

        return frameView;

    }
    
    string GetRollsPart(IFrameDto frame, bool isCurrentFrame=false, bool isLastFrame=false)
    {
        var firstRoll = frame.FirstRoll == null ? " " : frame.FirstRoll == 10 ? "X" : frame.FirstRoll.ToString();
        var secondRoll= frame.SecondRoll == null ? " " :
            frame.FirstRoll + frame.SecondRoll == 10 ? "/" : frame.SecondRoll.ToString();

        if (isCurrentFrame)
        {
            firstRoll = frame.FirstRoll == null ? "_" : firstRoll;
            secondRoll = frame.FirstRoll != null ? "_" : secondRoll;
            
        }

        var partialView = $"({firstRoll}|{secondRoll}";

        if (isLastFrame)
        {
            var thirdRoll = frame.ThirdRoll == null ? " " : frame.ThirdRoll == 10 ? "X" : frame.ThirdRoll.ToString();

            if (isCurrentFrame)
            {
                thirdRoll = frame.FirstRoll != null && frame.SecondRoll != null ? "_" : thirdRoll;
            }
            
            partialView += $"|{thirdRoll}";
        }

        partialView += ")";

        return partialView;

    }
}