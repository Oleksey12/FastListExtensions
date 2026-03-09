namespace ListExtensions
{
    using System;
    using System.Collections.Generic;

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

            int index = inputList.Count - 1;
            T item = inputList[index];
            inputList.RemoveAt(index);
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

        public static bool TryPop<T>(this List<T> inputList, out T item)
        {
            item = default;
            if (inputList == null)
            {
                throw new NullReferenceException("List cannot be null!");
            }

            if (inputList.Count == 0)
            {
                return false;
            }

            int index = inputList.Count - 1;
            item = inputList[index];
            inputList.RemoveAt(index);
            return true;
        }

        public static bool TryPopFirst<T>(this List<T> inputList, out T item)
        {
            item = default;
            if (inputList == null)
            {
                throw new NullReferenceException("List cannot be null!");
            }

            if (inputList.Count == 0)
            {
                return false;
            }

            int index = 0;
            item = inputList[index];
            inputList.RemoveAt(index);
            return true;
        }
    }
}
