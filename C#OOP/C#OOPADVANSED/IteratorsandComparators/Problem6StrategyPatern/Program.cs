
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        //List<Person> firstSet = new List<Person>();
        //List<Person> secondSet = new List<Person>();
        SortedSet<Person> firstSet = new SortedSet<Person>(new PersonComparatorByName());
        SortedSet<Person> secondSet = new SortedSet<Person>(new PersonComparatorByAge());

        var n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            var inputTokens = Console.ReadLine().Split(' ').ToArray();
            var person = new Person(inputTokens[0], int.Parse(inputTokens[1]));
            firstSet.Add(person);
            secondSet.Add(person);
        }

        PersonComparatorByAge ageComparer = new PersonComparatorByAge();
        PersonComparatorByName nameComparer = new PersonComparatorByName();

        //firstSet.Sort(nameComparer);
        
        Display(firstSet);
        //secondSet.Sort(ageComparer);
        Display(secondSet);
    }

    private static void Display(IEnumerable<Person> list)
    {
        
        foreach (var s in list)
        {
            Console.WriteLine(s);
        }
    }
}

