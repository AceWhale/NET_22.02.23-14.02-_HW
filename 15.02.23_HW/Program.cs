using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _15._02._23_HW
{
    internal class Program
    {
        class Book : IComparable, ICloneable
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public Book(string title, string author)
            {
                Title = title;
                Author = author;
            }
            public Book() : this("", "") { }
            public void Print()
            {
                Console.WriteLine($"Name: {Title}\nAuthor: {Author}\n");
            }
            public int CompareTo(object obj)
            {
                if (obj is Book)
                    return Title.CompareTo((obj as Book).Title);
                throw new NotImplementedException();
            }
            public class SortByTitle : IComparer
            {
                int IComparer.Compare(object obj1, object obj2)
                {
                    if (obj1 is Book && obj2 is Book)
                        return (obj1 as Book).Title.CompareTo((obj2 as Book).Title);

                    throw new NotImplementedException();
                }
            }
            public class SortByAuthor : IComparer
            {
                int IComparer.Compare(object obj1, object obj2)
                {
                    if (obj1 is Book && obj2 is Book)
                        return (obj1 as Book).Author.CompareTo((obj2 as Book).Author);

                    throw new NotImplementedException();
                }
            }
            public object Clone()
            {
                return new Book(Title, Author);
            }
        }

        class Library : IEnumerable
        {
            public Book[] list;
            public Library(int len)
            {
                list = new Book[len];
                for (int i = 0; i < len; i++)
                    list[i] = new Book();
            }
            public Library() : this(1) { }
            public Library(Book[] books)
            {
                list = new Book[books.Length];
                for (int i = 0; i < books.Length; i++)
                    list[i] = new Book(books[i].Title, books[i].Author);
            }
            public void Print()
            {
                for (int i = 0; i < list.Length; i++)
                    list[i].Print();
            }
            public IEnumerator GetEnumerator()
            {
                for (int i = 0; i < list.Length; i++)
                    yield return list[i];
            }
        }

        static void Main(string[] args)
        {
            Book[] arr = new Book[3];
            arr[0] = new Book("Ведьмак", "Сопковский");
            arr[1] = new Book("Гарри Поттер", "Роулинг");
            arr[2] = new Book("Алиса в Стране чудес", "Кэрролл");
            Library l = new Library(arr);
            Array.Sort(arr, new Book.SortByTitle());
            foreach (Book temp in arr)
                temp.Print();
            Console.WriteLine("\n\n");
            Array.Sort(arr, new Book.SortByAuthor());
            l = new Library(arr);
            foreach (Book temp in l)
                temp.Print();
        }
    }
}
