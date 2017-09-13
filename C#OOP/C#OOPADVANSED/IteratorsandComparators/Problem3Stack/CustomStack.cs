using System;
using System.Collections;
using System.Collections.Generic;

public class CustomStack<T> : IEnumerable<T>
{
    private readonly List<T> myList;
    public CustomStack()
    {
        this.myList = new List<T>();   
    }

    //public IReadOnlyList<T> MyList { get; private set; }

    public void Push(T[] elements)
    {
        this.myList.AddRange(elements);
    }

    public void Pop()
    {
        try
        {
            this.myList.RemoveAt(this.myList.Count - 1);
        }
        catch (Exception)
        {
            Console.WriteLine("No elements");
        }
       
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.myList.Count-1; i >= 0; i--)
        {
            yield return this.myList[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

