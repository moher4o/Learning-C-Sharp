using System.Collections;
using System.Collections.Generic;

public class Library : IEnumerable<Book>
{
    private SortedSet<Book> Books;

    public Library(params Book[] books)
    {
        this.Books = new SortedSet<Book>(books, new BookComparator()); 
    }

    public IEnumerator<Book> GetEnumerator()
    {
        return new LibraryIterator(this.Books);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class LibraryIterator : IEnumerator<Book>
    {
        private readonly List<Book> books;
        private int currentIndex;

        public LibraryIterator(IEnumerable<Book> books)
        {
            this.Reset();
            this.books = new List<Book>(books);
        }
        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            this.currentIndex++;
            return this.currentIndex < this.books.Count;
        }

        public void Reset()
        {
            this.currentIndex = -1;
          
        }

        public Book Current
        {
            get => this.books[this.currentIndex];
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }
    }
}

