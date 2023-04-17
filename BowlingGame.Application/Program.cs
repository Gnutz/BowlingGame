using BowlingGame.Frame;
using BowlingGame.RollProvider;
using BowlingGame.ScoringStrategy;

namespace BowlingGame.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scorer = new TraditionalScoreStrategy();
            var rollProvider = new ManualRollProvider();
            var game = new Game.BowlingGame(scorer, rollProvider);
            var presenter = new BowlingScorePresenter();
            
            presenter.DisplayGame(game.GetFrames());
            while (!game.IsGameCompleted())
            { 
                game.RollBowlingBall(); 
                displayer.DisplayGame(game.GetFrames());
            }

        }

       
    }
}
