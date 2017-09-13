   using System;
   using System.Collections;
   using System.Collections.Generic;

public class ListyIterator<T> : IEnumerable<T>
{
    private int currentIndex;
    private readonly List<T> myList;

    public ListyIterator(params T[] mylist)
    {
        this.myList = new List<T>(mylist);
        this.currentIndex = 0;
    }

    public bool Move()
    {
        if (this.currentIndex < this.myList.Count-1)
        {
            this.currentIndex++;
            return true;
        }
        
        return false;
    }

    public bool HasNext()
    {
        if (this.currentIndex < this.myList.Count-1)
        {
            return true;
        }
        return false;
    }

    public void Print()
    {
        try
        {
            Console.WriteLine(this.myList[this.currentIndex]);
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid Operation!");
            
        }
        
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.myList.Count; i++)
        {
            yield return this.myList[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

