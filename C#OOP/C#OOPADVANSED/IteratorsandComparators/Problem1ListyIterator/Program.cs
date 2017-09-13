using System;
using System.Linq;
using System.Text;

public class Program
{
    static void Main()
    {
        string input;
        input = Console.ReadLine();
        var inputTokens = input.Split(new [] {' ', '\t', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
        var myCollection = new ListyIterator<string>(inputTokens.Skip(1).ToArray());
        while ((input = Console.ReadLine()) != "END")
        {
            inputTokens = input.Split(' ');
            switch (inputTokens[0])
            {
                case "Move":
                    Console.WriteLine(myCollection.Move());
                    break;
                case "HasNext":
                    Console.WriteLine(myCollection.HasNext());
                    break;
                case "Print":
                    myCollection.Print();
                    break;
                case "PrintAll":
                    Console.WriteLine(string.Join(" ", myCollection));
                    break;

            }
        }
    }
}

