using BowlingGame.Frame;
using BowlingGame.ScoringStrategy;

namespace BowlingGame.Test.Frame;

public class FrameUnitTest
{
    
    //This is not a true unit test of the Frame entity class as it uses an instance of traditional scoring strategy
    //for it's IScoringStrategy dependency, but chose to do it this way because I thought the cohesion of scoring a frame
    //made sense in this cadse this way.
    //The tests were made as red/green tests for the default common case first of playing a tradition game of bowling

    private IScoringStrategy _scorer;
    private IFrame _uut;
    [SetUp]
    public void SetUp()
    {
        _scorer = new TraditionalScoreStrategy();
        _uut = new BowlingGame.Frame.Frame(scorer: _scorer);
    }

    [Test]
    public void AddRoll_FrameIsAlreadyFilled_ThrowsBowlingGameException()
    {
        //arrange
        _uut.AddRoll(2);
        _uut.AddRoll(5);

        //act and assert
        Assert.Throws<BowlingGameException>(() => _uut.AddRoll(3));
    }
    
    [Test]
    public void AddRoll_IsAStrike_TypeIsStrike_TryingASecondRollThrowsBowlingGameException()
    {
        //act
        _uut.AddRoll(10);
        
        //assert
        Assert.That(_uut.Type, Is.EqualTo(FrameType.Strike));
        Assert.Throws<BowlingGameException>(() => _uut.AddRoll(2));

    }
    
    [TestCase(1, 9)]
    [TestCase(3, 7)]
    [TestCase(5, 5)]

    public void AddRoll_IsASpare_TypeIsSpare_TryingAThirdRollThrowsBowlingGameException(int firstRoll, int secondRoll)
    {
        _uut.AddRoll(firstRoll);
        _uut.AddRoll(secondRoll);
        
        //assert
        Assert.That(_uut.Type, Is.EqualTo(FrameType.Spare));
        Assert.Throws<BowlingGameException>(() => _uut.AddRoll(2));
    }
    
    [TestCase(1, 7)]
    [TestCase(3, 5)]
    [TestCase(5, 2)]

    public void AddRoll_IsNonMarkedFrame_TypeIsNonMark_TryingAThirdRollThrowsBowlingGameException(int firstRoll, int secondRoll)
    {
        _uut.AddRoll(firstRoll);
        _uut.AddRoll(secondRoll);
        
        //assert
        Assert.That(_uut.Type, Is.EqualTo(FrameType.NonMark));
        Assert.Throws<BowlingGameException>(() => _uut.AddRoll(2));
    }
    
    [Test]
    public void TotalScore_IsCumulative_TwoFramesWithScoreOf5Is10TotalScoreOnSecondFrame()
    {
        var firstFrame = new BowlingGame.Frame.Frame(_scorer);
        firstFrame.AddRoll(0);
        firstFrame.AddRoll(5);
        var secondFrame = new BowlingGame.Frame.Frame(_scorer);
        firstFrame.Next = secondFrame;
        secondFrame.Previous = firstFrame;
        secondFrame.AddRoll(0);
        secondFrame.AddRoll(5);

        Assert.That(firstFrame.FrameScore, Is.EqualTo(5));
        Assert.That(firstFrame.TotalScore, Is.EqualTo(5));
        Assert.That(secondFrame.FrameScore, Is.EqualTo(5));
        Assert.That(secondFrame.TotalScore, Is.EqualTo(10));

    }
    
    [Test]
    public void FrameScore_RollingASeriesOfStrike2_8_6_TotalScoreForTheLastFrameIs36()
    {
        var firstFrame = new BowlingGame.Frame.Frame(_scorer);
        firstFrame.AddRoll(10);
        var secondFrame = new BowlingGame.Frame.Frame(_scorer, isLastFrame:true);
        firstFrame.Next = secondFrame;
        secondFrame.Previous = firstFrame;
        secondFrame.AddRoll(2);
        secondFrame.AddRoll(8);
        secondFrame.AddRoll(6);

        Assert.That(firstFrame.FrameScore, Is.EqualTo(20));
        Assert.That(firstFrame.TotalScore, Is.EqualTo(20));
        Assert.That(secondFrame.FrameScore, Is.EqualTo(16));
        Assert.That(secondFrame.TotalScore, Is.EqualTo(36));
    }

    [Test]
    public void AddRoll_PinsKnockedDownIsNegative_ThrowsArgumentOutOdRangeException()
    {
        //act and assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _uut.AddRoll(-3));
    }
    
    
}