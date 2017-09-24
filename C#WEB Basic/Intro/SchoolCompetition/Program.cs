using System.Collections.Generic;
using System.Linq;

namespace SchoolCompetition
{
    using System;

    class Program
    {
        static void Main()
        {
            var studentPoints = new Dictionary<string, int>();
            var studentCategories = new Dictionary<string,SortedSet<string>>();
            string input = string.Empty;
            while (!((input = Console.ReadLine()) == "END"))
            {
                string[] inputTokens = input.Split(' ');
                string name = inputTokens[0];
                string category = inputTokens[1];
                int points = int.Parse(inputTokens[2]);

                if (!studentPoints.ContainsKey(name))
                {
                    studentPoints.Add(name,0);
                }
                if (!studentCategories.ContainsKey(name))
                {
                    studentCategories.Add(name, new SortedSet<string>());
                }

                studentPoints[name] += points;
                studentCategories[name].Add(category);
            }

            var studentPointsOrdered = studentPoints
                                        .OrderByDescending(s => s.Value)
                                        .ThenBy(s => s.Key);
            foreach (var student in studentPointsOrdered)
            {
                var categories = $"[{string.Join(", ", studentCategories[student.Key])}]";
                Console.WriteLine($"{student.Key}: {student.Value} {categories}");
            }
        }
    }
}