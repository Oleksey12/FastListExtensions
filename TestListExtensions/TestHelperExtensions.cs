namespace ListExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class TestHelperExtensions
    {
        private enum Elements
        {
            All = -1
        }
        public static T Pop<T>(this List<T> inputList)
        {
            if (inputList == null)
            {
                throw new NullReferenceException("List cannot be null!");
            }

            if (inputList.Count == 0)
            {
                throw new InvalidOperationException("Can't pop from empty list!");
            }

            int lastIndex = inputList.Count - 1;
            T item = inputList[lastIndex];
            inputList.RemoveAt(lastIndex);
            return item;
        }

        public static T PopFirst<T>(this List<T> inputList)
        {
            if (inputList == null)
            {
                throw new NullReferenceException("List cannot be null!");
            }

            if (inputList.Count == 0)
            {
                throw new InvalidOperationException("Can't pop from empty list!");
            }

            int index = 0;
            T item = inputList[index];
            inputList.RemoveAt(index);
            return item;
        }
    }
}
