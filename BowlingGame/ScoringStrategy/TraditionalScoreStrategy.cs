using BowlingGame.Frame;

namespace BowlingGame.ScoringStrategy;

public class TraditionalScoreStrategy: IScoringStrategy
{
    public int ScoreFrame(IFrame frame)
    {

        switch (frame.Type)
        {
            case FrameType.Strike:
                return CalculateStrikeScore(frame);
            case FrameType.Spare:
                return CalculateSpareScore(frame);
            default:
                //OpenFrame and NonMarked
                return frame.Rolls.Sum(roll => roll ?? 0);
        }
    }

    private int CalculateSpareScore(IFrame frame)
    {
        //Ten plus bonus of the next roll
        var nextRoll = GetNextRoll(frame);
        return 10 + nextRoll;
    }

    private int GetNextRoll(IFrame frame)
    {
        var nextFrame = frame.Next;
        if (nextFrame == null)
        {
            //this is the last frame 1 additional roll has been made which will be the next roll
            return frame.Rolls.LastOrDefault() ?? 0;
        }

        //this frame isn't the last frame, so the next roll is gotten from the next frame
        return nextFrame.Rolls.FirstOrDefault() ?? 0;
    }
    
    private int CalculateStrikeScore(IFrame frame)
    {
        //10 plus the bonus of the next two rolls
        var nextTwoRolls = GetNextTwoRolls(frame);
        return 10 + nextTwoRolls.Sum(roll => roll ?? 0);
    }

    private List<int?> GetNextTwoRolls(IFrame frame)
    {
        var nextFrame = frame.Next;
        if (nextFrame == null)
        {
            //this is the last frame 2 additional rolls has been made
            return frame.Rolls.Skip(1).Take(2).ToList();
        }
        
        //this frame isn't the last frame
        var nextFrameType = nextFrame.Type;
        if (nextFrameType == FrameType.Strike)
        {
            //The next frame is also a strike, so we need another frame to calculate the bonus
            var twoFrameFromMe = nextFrame.Next;
            if (twoFrameFromMe != null)
            {
                //two given strikes and then the pins on the first roll of the third frame
                var firstRoll = nextFrame.Rolls.ElementAtOrDefault(0);
                var secondRoll = twoFrameFromMe.Rolls.ElementAtOrDefault(0);
                return new List<int?>() { firstRoll, secondRoll };
            }
            else
            {
                // the next frame is the last frame so we take the two first rolls as the bonus
                return nextFrame.Rolls.Take(2).ToList();
            }
        }
        else
        {
            //if the next frame is spare, nonMarked or still a OpenFrame we try to get the rolls of that frame
            return nextFrame.Rolls.Take(2).ToList();
        }
    }
}