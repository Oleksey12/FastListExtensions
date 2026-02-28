namespace LE
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class LE
    {
        /// <summary>
        /// Finds the maximum value of the list
        /// </summary>
        /// <remarks>
        /// This method is <b>not thread-safe</b>. If the list can be modified concurrently 
        /// you must synchronize access, for example with a <see cref="lock"/>.
        /// The method uses <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood,
        /// any concurrent list modification can cause unpredicted behavior. 
        /// </remarks>
        /// <param name="data">Input list</param>
        /// <returns>Maximum value of the list</returns>
        /// <exception cref="ArgumentException">Throws exception if list is empty</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FindMax<T>(this List<T> data) where T : IComparable<T>
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("The input list is empty");
            }

            T max = data[0];

            if (data.Count == 1)
            {
                return max;
            }

            Comparer<T> comparer = Comparer<T>.Default;
            Span<T> values = CollectionsMarshal.AsSpan(data);

            for (int i = 1; i < values.Length; i++)
            {
                ref T current = ref values[i]; 
                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return max;
        }

        /// <summary>
        /// Finds the maximum value of the list
        /// </summary>
        /// <remarks>
        /// This method is <b>not thread-safe</b>. If the list can be modified concurrently 
        /// you must synchronize access, for example with a <see cref="lock"/>.
        /// The method uses <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood,
        /// any concurrent list modification can cause unpredicted behavior. 
        /// </remarks>
        /// <param name="data">Input list</param>
        /// <returns>Maximum value of the list</returns>
        /// <exception cref="ArgumentException">Throws exception if list is empty</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SimpleMax<T>(this List<T> data) where T : IComparisonOperators<T, T, bool>
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("The input list is empty");
            }

            T max = data[0];

            if (data.Count == 1)
            {
                return max;
            }

            Comparer<T> comparer = Comparer<T>.Default;
            Span<T> values = CollectionsMarshal.AsSpan(data);

            for (int i = 1; i < values.Length; i++)
            {
                ref T current = ref values[i];
                if (current > max)
                {
                    max = current;
                }
            }

            return max;
        }
    }
}
