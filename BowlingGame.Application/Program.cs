using BowlingGame.Frame;
using BowlingGame.RollProvider;
using BowlingGame.ScoringStrategy;

namespace BowlingGame.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please select how you wish to generate rolls for this bowling game simulation:");
            Console.WriteLine("Please enter '1' to enter the rolls yourself");
            Console.WriteLine("Please enter '2' to get automated rolls");
            var choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            var scorer = new TraditionalScoreStrategy();
            IRollProvider rollProvider = null;
            int delay = 2000;

            switch (choice)
            {
                case '1':
                    rollProvider = new ManualRollProvider();
                    delay = 100;
                    break;
                case '2':
                    rollProvider = new RandomRollProvider();
                    break;
                default:
                    break;
            }
            var game = new Game.BowlingGame(scorer, rollProvider);
            var presenter = new BowlingScorePresenter();
            
            presenter.DisplayGame(game.GetFrames());
            while (!game.IsGameCompleted())
            { 
                Thread.Sleep(delay);
                game.RollBowlingBall(); 
                presenter.DisplayGame(game.GetFrames());
            }

        }

       
    }
}
