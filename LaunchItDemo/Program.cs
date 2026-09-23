namespace LaunchItDemo;

internal class Program
    {
    static void Main(string[] args)
        {
        Console.WriteLine("=== LaunchIt Demo ===");
        Console.WriteLine("Enter a path or URL to launch:");
        var input = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine($"You entered: {input}");

        var StopProgram = new LaunchIt.Resolvers.KillResolver();

        }
    }

