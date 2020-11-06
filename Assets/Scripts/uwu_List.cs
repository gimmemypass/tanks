using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class uwu_List<T> : IEnumerable<T>
    {
        private T[] uwu_Array = new T[1];
        public int Size { get; set; } = 0;

        public uwu_List() { }
        public void Add(T data)
        {
            Size++;
            if (Size > uwu_Array.Length)
                Array.Resize(ref uwu_Array, uwu_Array.Length * 2);
            uwu_Array[Size - 1] = data;
        }
        public void AddElements(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                this.Add(array[i]);
        }
        public void DeleteAt(int ind)
        {
            if (ind > Size || ind < 0 || Size == 0)
                throw new Exception($"Error uwu_Array.DeleteAt operation: Size = {Size}, Available = {uwu_Array.Length}, index = {ind}");
            Size--;
            for (int i = ind; i < Size; i++)
                uwu_Array[i] = uwu_Array[i + 1];
            if (Size < uwu_Array.Length / 2)
                Array.Resize(ref uwu_Array, uwu_Array.Length / 2);
        }
        public void InsertAt(T data, int ind)
        {
            if (ind > Size || ind < 0)
                throw new Exception($"Error uwu_Array.InsertAt operation: Size = {Size}, Available = {uwu_Array.Length}, index = {ind}");
            Size++;
            if (Size > uwu_Array.Length)
                Array.Resize(ref uwu_Array, uwu_Array.Length * 2);
            for (int i = ind + 1; i < Size; i++)
                uwu_Array[i] = uwu_Array[i - 1];
            uwu_Array[ind] = data;
        }
        public T At(int ind)
        {
            if (ind > Size || ind < 0 || Size == 0)
                throw new Exception($"Error uwu_Array.At operation: Size = {Size}, Available = {uwu_Array.Length}, index = {ind}");
            return uwu_Array[ind];
        }
        public void Set(T data, int ind)
        {
            if (ind > Size || ind < 0 || Size == 0)
                throw new Exception($"Error uwu_Array.Set operation: Size = {Size}, Available = {uwu_Array.Length}, index = {ind}");
            uwu_Array[ind] = data;
        }
        public T this[int ind]
        {
            get { return At(ind); }
            set { Set(value, ind); }
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
                yield return uwu_Array[i];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}