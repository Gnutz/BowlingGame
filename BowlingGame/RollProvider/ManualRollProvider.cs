namespace BowlingGame.RollProvider;

public class ManualRollProvider : IRollProvider
{
    public int NextRoll()
    {
        Console.WriteLine("Input the rolled number of pins: ");
        return Convert.ToInt32(Console.ReadLine());
    }
}