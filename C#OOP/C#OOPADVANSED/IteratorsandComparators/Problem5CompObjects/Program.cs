
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string input;
        IList<Person> peoples = new List<Person>();
        while ((input = Console.ReadLine()) != "END")
        {
            var inputTokens = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var person = new Person(inputTokens[0],int.Parse(inputTokens[1]),inputTokens[2]);
            peoples.Add(person);
        }
        input = Console.ReadLine();
        var index = int.Parse(input)-1;
        var equal = peoples.Count(p => peoples[index].CompareTo(p) == 0);
        if (equal == 1)
        {
            Console.WriteLine("No matches");
        }
        else
        {
            Console.WriteLine($"{equal} {peoples.Count - equal} {peoples.Count}");
        }

    }
}

