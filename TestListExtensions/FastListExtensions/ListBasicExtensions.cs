namespace FastListExtensions
{
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Extensions for simplifying List<T> operations
    /// </summary>
    internal static class ListBasicExtensions
    {
        /// <summary>
        /// Removes and returns last element of the list
        /// </summary>
        /// <param name="data">Target list</param>
        /// <returns>Last element of the list</returns>
        /// <exception cref="NullReferenceException">Throws when list is null</exception>
        /// <exception cref="InvalidOperationException">Throws when list is empty</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Pop<T>(this List<T> data)
        {
            if (data == null)
            {
                throw new NullReferenceException("List cannot be null!");
            }

            if (data.Count == 0)
            {
                throw new InvalidOperationException("Can't pop from empty list!");
            }

            int index = data.Count - 1;
            T item = data[index];
            data.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// Removes and returns first element of the list
        /// </summary>
        /// <param name="data">Target list</param>
        /// <returns>First element of the list</returns>
        /// <exception cref="NullReferenceException">Throws when list is null</exception>
        /// <exception cref="InvalidOperationException">Throws when list is empty</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopFirst<T>(this List<T> data)
        {
            if (data == null)
            {
                throw new NullReferenceException("List cannot be null!");
            }

            if (data.Count == 0)
            {
                throw new InvalidOperationException("Can't pop from empty list!");
            }

            int index = 0;
            T item = data[index];
            data.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// Safe method for removing and returning the last element of the list
        /// </summary>
        /// <param name="data">Target list</param>
        /// <param name="result">Extracted element</param>
        /// <returns>Was operation performed or not</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPop<T>(this List<T> data, out T result)
        {
            result = default;
            if (data == null)
            {
                return false;
            }

            if (data.Count == 0)
            {
                return false;
            }

            int index = data.Count - 1;
            result = data[index];
            data.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Safe method for removing and returning the first element of the list
        /// </summary>
        /// <param name="data">Target list</param>
        /// <param name="result">Extracted element</param>
        /// <returns>Was operation performed or not</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPopFirst<T>(this List<T> data, out T result)
        {
            result = default;
            if (data == null)
            {
                return false;
            }

            if (data.Count == 0)
            {
                return false;
            }

            int index = 0;
            result = data[index];
            data.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Safe method for finding element by function
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="function">Expression for searching element</param>
        /// <param name="result">Extracted element</param>
        /// <returns>Was operation performed or not</returns>
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
        private static bool TryFindSpanImpl<T>(this List<T> data, Func<T, bool> function, out T result)
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

        /// <summary>
        /// Safe method for upcasting classes  
        /// </summary>
        /// <param name="value">Target class</param>
        /// <param name="result">Converted class</param>
        /// <returns>Was operation performed or not</returns>
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
