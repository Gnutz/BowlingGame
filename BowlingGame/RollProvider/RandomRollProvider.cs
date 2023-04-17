namespace BowlingGame.RollProvider;

public class RandomRollProvider : IRollProvider
{
    public int NextRoll() =>  new Random().Next(0, 11);
};
