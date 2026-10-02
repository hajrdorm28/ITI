using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7_1
{
    public class DynamicArray<T>
    {
        private T[] _items;
        private int _length;

        public int Length => _length;
        public int Capacity => _items.Length;

        public DynamicArray() 
        { 
            _items = new T[2];
            _length = 0;
        }
        public void Push(T item)
        {
            if (_length == Capacity)
            {
                T[] newArr = new T[Capacity * 2];
                for (int i = 0; i < _length; i++)
                    newArr[i] = _items[i];
                _items = newArr;
            }
            _items[_length++] = item;
        }
        public T Pop()
        {
            if ( _length == 0) 
                throw new InvalidOperationException("Error: Array is empty!");

            return _items[--_length];
        }

        public T this[int index]
        {
            get 
            { 
                if( index < 0 || index >= _length)
                    throw new IndexOutOfRangeException("Invalid index");
                return _items[index]; 
            }
        }
    }
}
