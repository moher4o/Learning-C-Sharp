using System;
public class Person : IComparable<Person>
{
    public Person(string name, int age, string town)
    {
        this.Name = name;
        this.Age = age;
        this.Town = town;   
    }
    public string Name { get; }
    public int Age { get; }
    public string Town { get; }

    public int CompareTo(Person otherPerson)
    {
        int result = this.Name.CompareTo(otherPerson.Name);
        if (result == 0)
        {
            result = this.Age.CompareTo(otherPerson.Age);
        }
        if (result == 0)
        {
            result = this.Town.CompareTo(otherPerson.Town);
        }
        return result;
    }
}

