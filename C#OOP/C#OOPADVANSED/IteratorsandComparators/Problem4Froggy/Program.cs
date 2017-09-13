
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var inputTokens = Console.ReadLine().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        Frog lake = new Frog(inputTokens);
        Console.WriteLine(string.Join(", ", lake));
    }
}

