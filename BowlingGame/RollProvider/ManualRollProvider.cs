namespace BowlingGame.RollProvider;

public class ManualRollProvider : IRollProvider
{
    public int NextRoll()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Input the rolled number of pins: ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("You need to provide an integer value");
            }
        }
    }
}