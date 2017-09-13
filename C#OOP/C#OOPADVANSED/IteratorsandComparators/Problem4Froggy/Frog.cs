
using System.Collections;
using System.Collections.Generic;

public class Frog :IEnumerable<int>
{
    private readonly List<int> stones;

    public Frog(params int[] myList)
    {
        this.stones = new List<int>(myList);   
    }


    public IEnumerator<int> GetEnumerator()
    {
        //return new Lake(this.stones);
        for (int i = 0; i < this.stones.Count; i += 2)
        {
            yield return this.stones[i];
        }

        int lastOddIndex = this.stones.Count - 1;
        if (lastOddIndex % 2 == 0)
        {
            lastOddIndex--;
        }

        for (int i = lastOddIndex; i > 0; i -= 2)
        {
            yield return this.stones[i];
        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class Lake : IEnumerator<int>
    {
        private readonly List<int> data;
        private int currentIndex;

        public Lake(IEnumerable<int> data)
        {
            this.Reset();
            this.data = new List<int>(data);   
        }
        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (((this.currentIndex + 2)<this.data.Count) && this.currentIndex % 2 == 0)
            {
                this.currentIndex += 2;
                return true;
            }
            else if ((this.currentIndex % 2 != 0) && ((this.currentIndex - 2) > 0))
            {
                this.currentIndex -= 2;
                return true;
            }
            else if (this.currentIndex % 2 == 0 && this.data.Count == (this.currentIndex + 2))
            {
                this.currentIndex += 1;
                return true;
            }
            else if (this.currentIndex % 2 == 0 && this.data.Count == (this.currentIndex + 1))
            {
                this.currentIndex -= 1;
                return true;
            }
            return false;

        }

        public void Reset()
        {
            this.currentIndex = -2;
        }

        public int Current
        {
            get => this.data[this.currentIndex];
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }
    }
}
