
using System.Collections.Generic;
public class PersonComparatorByAge : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        return x.Age.CompareTo(y.Age);
        //return x.Age - y.Age;
    }
}

