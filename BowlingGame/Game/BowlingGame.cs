using BowlingGame.Frame;
using BowlingGame.RollProvider;
using BowlingGame.ScoringStrategy;

namespace BowlingGame.Game;

public class BowlingGame : IBowlingGame
{
    private readonly IRollProvider _rollProvider;
    private readonly IScoringStrategy _scorer;
    private readonly IFrame _head;
    private IFrame _currentFrame;
    private int _numberOfFrames;

    public BowlingGame(IScoringStrategy? scorer=null, IRollProvider? rollProvider=null)
    {
        _rollProvider = rollProvider ?? new RandomRollProvider();
        _scorer = scorer ?? new TraditionalScoreStrategy();
        _head = new Frame.Frame(_scorer);
        _numberOfFrames = 1;
        _currentFrame = _head;
    }
    
    public int TotalScore => _currentFrame?.TotalScore ?? 0;

    public void RollBowlingBall()
    {
        
        // if currentFrame is not Open throw exception
        if (!_currentFrame.IsOpenFrame) throw new BowlingGameException("The current frame is already closed"); 
        
        AddRoll();
        
        // check if roll closed the frame
        if(!_currentFrame.IsOpenFrame && _numberOfFrames <= 9)
        {
            var nextFrame = new Frame.Frame(scorer: _scorer, isLastFrame: _numberOfFrames >= 9);
            
            //linking the frames
            _currentFrame.Next = nextFrame;
            nextFrame.Previous = _currentFrame;
            _currentFrame = nextFrame;
            _numberOfFrames++;
        }
    }

    private void AddRoll()
    {
        var currentRollsRecordedOnFrame = _currentFrame.Rolls.Count;
        do
        {
            try
            {
                //generate a roll
                var newRoll = _rollProvider.NextRoll();
                _currentFrame.AddRoll(newRoll);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        } while (_currentFrame.Rolls.Count <= currentRollsRecordedOnFrame);
    }

    public bool IsGameCompleted()
    {
        return _numberOfFrames == 10 && !_currentFrame.IsOpenFrame;
    }
    
    public List<IFrameDto> GetFrames()
    { 
        var framesSeries = new List<IFrameDto>();
        var frameToConvert = _head;
        while (frameToConvert != null)
        {
            framesSeries.Add(frameToConvert.ToFrameDto());
            frameToConvert = frameToConvert.Next;
        }

        while (framesSeries.Count < 10)
        {
            framesSeries.Add(new FrameDto());
        }
        return framesSeries;
    }
}