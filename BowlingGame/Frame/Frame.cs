using BowlingGame.ScoringStrategy;

namespace BowlingGame.Frame;

public class Frame : IFrame
{
    // ReSharper disable once InconsistentNaming
    private int MAX_ROLLS => _isLastFrame ? 3 : 2;

    private readonly List<int?> _rolls = new List<int?>();
    private readonly IScoringStrategy _scorer;
    private int _pinsLeft = 10;
    private readonly bool _isLastFrame;
    public IFrame? Next { get; set; }

    public IFrame? Previous { get; set; }
    
    public Frame(IScoringStrategy? scorer=null, bool isLastFrame=false)
    {
        _scorer = scorer ?? new TraditionalScoreStrategy();
        _isLastFrame = isLastFrame;
    }

    public bool IsOpenFrame => Type == FrameType.OpenFrame;

    public void AddRoll(int pinsKnockedDown)
    {
        if (Type != FrameType.OpenFrame) throw new BowlingGameException("This frame is already closed");
        
        // add roll
        ValidateRollInRange(pinsKnockedDown:pinsKnockedDown);
        _rolls.Add(pinsKnockedDown);
        _pinsLeft -= pinsKnockedDown;
        
        if(_pinsLeft == 0 && _isLastFrame)  ResetPins();
    }

    public List<int?> Rolls => _rolls;
    
    private void ResetPins()
    {
        _pinsLeft = 10;
    }

    public int? FrameScore => _scorer.ScoreFrame(this);

    public int? TotalScore => (Previous?.TotalScore ?? 0) + FrameScore;

    public FrameType Type
    {
        get
        {
            var frametype = FrameType.OpenFrame;
            if (Rolls.Count <= 0) return frametype;
            if (Rolls.Count >= 1)
            {
                if (Rolls[0] == 10) frametype = FrameType.Strike;
            }
            if (Rolls.Count >= 2) { 
                if (Rolls[0] + Rolls[1] == 10) frametype = FrameType.Spare;
                if (Rolls[0] + Rolls[1] < 10) frametype = FrameType.NonMark;
                
            }
            if (_isLastFrame && (frametype == FrameType.Strike || frametype == FrameType.Spare) && Rolls.Count < MAX_ROLLS)
            {
                frametype = FrameType.OpenFrame;
            }

            return frametype;
        }
    }
    public IFrameDto ToFrameDto()
    {
        return new FrameDto(Rolls.FirstOrDefault(), 
            Rolls.ElementAtOrDefault(1), 
            Rolls.ElementAtOrDefault(2),
            FrameScore,
            TotalScore,
            IsOpenFrame);
    }

    private void ValidateRollInRange(int pinsKnockedDown)
    {
        if (pinsKnockedDown < 0) throw new ArgumentOutOfRangeException(nameof(pinsKnockedDown), "You can not bowl negative pins");
        if (pinsKnockedDown > _pinsLeft) throw new ArgumentOutOfRangeException(nameof(pinsKnockedDown), $"You can not bowl more pins than the {_pinsLeft} left on the frame");
    }
}