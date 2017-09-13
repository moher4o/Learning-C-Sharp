using System;
using System.Linq;

class Program
{
    static void Main()
    {
        CustomStack<int> myStack = new CustomStack<int>();
        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            var inputTokens = input.Split(new[] { " ", ", " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            switch (inputTokens[0])
            {
                case "Push":
                    myStack.Push(inputTokens.Skip(1).Select(int.Parse).ToArray());
                    break;
                case "Pop":
                    myStack.Pop();
                    break;
            }
        }
        PrintForeach(myStack);
        PrintForeach(myStack);
    }

    private static void PrintForeach(CustomStack<int> myStack)
    {
        foreach (var item in myStack)
        {
            Console.WriteLine(item);
        }
    }
}

