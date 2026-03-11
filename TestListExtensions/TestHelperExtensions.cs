namespace ListExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class TestHelperExtensions
    {
        private enum Elements
        {
            All = -1
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPop<T>(this List<T> inputList, out T item)
        {
            item = default;
            if (inputList == null)
            {
                return false;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPopFirst<T>(this List<T> inputList, out T item)
        {
            item = default;
            if (inputList == null)
            {
                return false;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFind<T>(this List<T> data, Func<T, bool> function, out T result)
        {
            result = default;
            if (data == null)
            {
                return false;
            }

            if (function == null)
            {
                return false;
            }

            return TryFindSpanImpl(data, function, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFindSpanImpl<T>(this List<T> data, Func<T, bool> function, out T result)
        {
            result = default;
            Span<T> values = CollectionsMarshal.AsSpan(data);

            for (int i = 0; i < values.Length; i++) 
            {
                if (function(values[i]))
                {
                    result = values[i];
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryConvert<T, G>(this T value, out G result) where G : class
        {
            result = null;
            if (value == null)
            {
                return false;
            }

            return (result = value as G) != null;
        }
    }
}
