using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomList
{
    public class CustomList<T> : IList<T>
    {
        private Item<T> head;
        private int count;
        /// <summary>
        /// The property return first element of list 
        /// </summary>
        public Item<T> Head
        {
            get => head;
        }

        /// <summary>
        /// The property return number of elements contained in the CustomList
        /// </summary>
        public int Count
        {
            get => count;
            private set => count = value;
        }

        /// <summary>
        /// Gets a value indicating whether the IList is read-only.
        /// Make it always false
        /// </summary>
        public bool IsReadOnly => false;


        /// <summary>
        /// Constructor that gets params T as parameter       
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when values is null</exception>
        /// <param name="values"></param>
        public CustomList(params T[] values)
        {
            int counter = 0;
            if (values == null)
                throw new ArgumentNullException("values");
            
            foreach(var v in values)
            {
                counter++;
                this.Add(v);
            }

            this.Count = counter;
        }


        /// <summary>
        /// Constructor that gets Ienumerable collection as parameter       
        /// </summary>
        ///<exception cref="ArgumentNullException">Thrown when values is null</exception>
        /// <param name="values"></param>
        public CustomList(IEnumerable<T> values)
        {
            int counter = 0;

            if (values == null)
                throw new ArgumentNullException("values");
            
            foreach (var i in values)
            {
                counter++;
                this.Add(i);
            }
            Count = counter - 1;
        }

        /// <summary>
        /// Get or set data at the position.
        /// </summary>
        /// <param name="index">Position</param>
        /// <exception cref="IndexOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count)
                {
                    dynamic d = new IndexOutOfRangeException("index");
                    throw d;
                }

                Item<T> element = Head;
                int counter = 0;
                while (element.Next != null)
                {
                    if (counter == index)
                        break;
                    else
                    {
                        counter++;
                        element = element.Next;
                    }
                }

                return element.Data;
            }
            set
            {
                if (index < 0 || index > Count)
                {
                    dynamic d = new IndexOutOfRangeException("index");
                    throw d;
                }
                this.Insert(index, value);
            }
        }

        /// <summary>
        ///  Adds an object to the end of the CustomList.
        /// </summary>
        /// <param name="data">Object that should be added in the CustomList</param>
        /// <exception cref="ArgumentNullException">Throws when you try to add null</exception>
        public void Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException("item");
            }
            if (Head == null)
            {
                head = new Item<T>(item);
            }
            else
            {
                Item<T> element = Head;

                while (element.Next != null)
                {
                    element = element.Next;
                }
                Item<T> newNext = new Item<T>(item);
                element.Next = newNext;
            }
        }


        /// <summary>
        /// Removes all elements from the CustomList
        /// </summary>
        public void Clear()
        {
            this.head = null;
            Count = 0;
        }

        /// <summary>
        /// Determines whether an element is in the CustomList
        /// </summary>
        /// <param name="item">Object we check to see if it is on the CustomLIst</param>
        /// <returns>True if the element exists in the CustomList, else false</returns>
        public bool Contains(T item)
        {
            Item<T> element = Head;
            
            while(element != null)
            {
                if (element.Data.Equals(item))
                    return true;

                element = element.Next;
            }

            return false;
        }


        /// <summary>
        /// Removes the first occurrence of a specific object from the CustomList.
        /// </summary>
        /// <param name="item"> The object to remove from the CustomList</param>
        /// <returns>True if item is successfully removed; otherwise, false. This method also returns
        ///     false if item was not found in the CustomList.</returns>
        /// <exception cref="ArgumentNullException">Throws when you try to remove null</exception>
        public bool Remove(T item)
        {

            Item<T> prev = Head;

            if (item is null)
                throw new ArgumentNullException("item");
                
            if (!this.Contains(item))
                return false;
            
            
            Item<T> element = Head;

            while (element != null)
            {
                if (Head.Data.Equals(item) && head.Next != null)
                {
                        head = head.Next;
                        Count--;
                        break;
                } 
                else if(Head.Data.Equals(item) && head.Next == null)
                {
                    this.Clear();
                    break;
                            
                }
                else if (element.Data.Equals(item))
                {
                    prev.Next = (element.Next is null) ? (element.Next) : (null);   
                    Count--;
                    break;
                }
                prev = element;
                element = element.Next;
            }

            return true;
        }


        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first
        ///     occurrence within the CustomList.
        /// </summary>
        /// <param name="item">The object whose index we want to get </param>
        /// <returns>The zero-based index of the first occurrence of item within the entire CustomList,
        ///    if found; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            int counter = 0;
            if(!this.Contains(item))
            {
                return -1;
            } 
            else
            {
                Item<T> element = Head;

                while (element != null)
                {
                    if (element.Data.Equals(item))
                        break;
                    else
                    {
                        counter++;
                        element = element.Next;
                    }

                }

                return counter;
            }
        }


        /// <summary>
        /// Inserts an element into the CustomList at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        /// <exception cref="ArgumentNullException">Thrown when item is null</exception>
        public void Insert(int index, T item)
        {

            if (item == null)
                throw new ArgumentNullException("item");

            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException("index");

            Item<T> element = Head;
            Item<T> prev = Head;
            Item<T> newElement = new Item<T>(item);
            
            int counter = 0;

                while (prev != null)
                {
                if (index == 0)
                {
                    newElement.Next = Head;
                    head = newElement;
                    Count++;
                    break;
                }
                else if (index > 1 && counter == index)
                {

                        Count++;
                        prev.Next = newElement;
                        newElement.Next = element;
                        break;
                    
                }
                    counter++;
                    prev = element;
                    element = element.Next;

                } 
            
  
        }

        /// <summary>
        /// Removes the element at the specified index of the CustomList.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException("index");

            Item<T> element = Head;
            Item<T> prev;
            int counter = 0;
            if (index == 0)
            {
                head = head.Next;
                Count--;
            }
            else
            {
                prev = Head;

                while (element.Next != null)
                {

                    if (counter == index)
                    {
                        prev.Next = element.Next;
                        Count--;
                    }
                    prev = element;
                    element = element.Next;
                    counter++;
                }
            }
        }


        /// <summary>
        /// Copies the entire CustomList to a compatible one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements copied
        ///     from CustomList</param>
        /// <param name="arrayIndex">The zero-based index in the source System.Array at which
        ///   copying begins.</param>
        ///   <exception cref="ArgumentNullException">Array is null.</exception>
        ///   <exception cref="ArgumentException">The number of elements in the source CustomList is greater
        ///    than the number of elements that the destination array can contain</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException("array");

            if (this.Count > array.Length)
                throw new ArgumentException("array");

            int counter = 0;

            for(int i = arrayIndex; i < Count; i++)
            {

                array[i] = this[counter];
                counter++;
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the CustomList.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {

            Item<T> element = Head;
            while (element != null)
            {
                yield return element.Data;
                element = element.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
