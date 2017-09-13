
using System.Collections.Generic;
public class PersonComparatorByName : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        var result = x.Name.Length.CompareTo(y.Name.Length);
        if (result == 0)
        {
            result = x.Name.CompareTo(y.Name);
        }
        //int result = x.Name.Length - y.Name.Length;
        //if (result == 0)
        //{
        //    char xFirstLetter = char.ToLower(x.Name[0]);
        //    char yFirstLettr = char.ToLower(y.Name[0]);
        //    result = xFirstLetter.CompareTo(yFirstLettr);
        //}

        return result;

        
    }
}

