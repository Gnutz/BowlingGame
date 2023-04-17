using BowlingGame.Game;
using BowlingGame.RollProvider;
using BowlingGame.ScoringStrategy;
using NSubstitute;

namespace BowlingGame.Test.Game;

public class BowlingGameIntegrationTest
{
    private IScoringStrategy _scoringStrategy;
    private IRollProvider _rollProvider;
    private IBowlingGame _uut;
    
    [SetUp]
    public void SetUp()
    {
        _scoringStrategy = new TraditionalScoreStrategy();
        _rollProvider = Substitute.For<IRollProvider>();
        _uut = new BowlingGame.Game.BowlingGame(_scoringStrategy, _rollProvider);

    }
    
    [Test]
    public void TotalScore_After20GutterBalls_TotalScoreIs0()
    {
        _rollProvider.NextRoll().Returns(0);
        
        for (int i = 0; i < 20; i++)
        {
            _uut.RollBowlingBall();
        }
        
        Assert.That(_uut.TotalScore, Is.EqualTo(0));
    }
    
    
    //Test Perfect Game
    [Test]
    public void TotalScore_After12Strikes_TotalScoreIs300()
    {
        //arrange
        _rollProvider.NextRoll().Returns(10);

        //act
        for (int i = 0; i < 12; i++)
        {
            _uut.RollBowlingBall();
        }
        
        //assert
        Assert.That(_uut.TotalScore, Is.EqualTo(300));
    }
    
    [Test]
    public void IndexWasOutOfRangeTest()
    {
        var rolls = new List<int>(){
         3, 4, 7 , 2, 8, 2, 10, 10
        };
        
        //
        foreach (var roll in rolls)
        {
            _rollProvider.NextRoll().Returns(roll);
            _uut.RollBowlingBall();
        }
        
        //Assert
        Assert.That(_uut.TotalScore, Is.EqualTo(66));
    }
    
    [Test]
    public void TotalScore_AfterGameFromChallengeExample_TotalScoreIs133()
    {
        var rolls = new List<int>(){
            1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 1, 7, 3, 6, 4, 10, 2, 8, 6
        };
        
        //
        foreach (var roll in rolls)
        {
            _rollProvider.NextRoll().Returns(roll);
            _uut.RollBowlingBall();
        }
        
        //Assert
        Assert.That(_uut.TotalScore, Is.EqualTo(133));
    }
}