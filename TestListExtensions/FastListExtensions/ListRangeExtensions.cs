namespace FastListExtensions
{
    using System;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Performant extensions to make operations on the specific range
    /// </summary>
    public static class ListRangeExtensions
    {
        /// <summary>
        /// Simplifies sending signal to take all available elements
        /// </summary>
        public enum Elements
        {
            All = -1
        }

        /// <summary>
        /// Invoke function for every element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="action">Invoked function</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        public static void ForEachOnRange<T>(this List<T> data, Action<T> action, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (action == null)
            {
                throw new ArgumentNullException("Action function cannot be null");
            }

            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (elementsCount < 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be non-negative.");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            ForEachOnRangeSpanImpl(data, action, startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEachOnRangeSpanImpl<T>(this List<T> data, Action<T> action, int startIndex, int elementsCount)
        {
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            for (int i = 0; i < elementsCount; i++)
            {
                action(values[i]);
            }
        }

        /// <summary>
        /// Applies cumulative function on the given range, starting from startValue
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="aggregateFunction">Cumulative function</param>
        /// <param name="startValue">The starting value for making calculations</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Result of cumulative calculations</returns>
        public static G AggregateOnRange<T, G>(this List<T> data, Func<G, T, G> aggregateFunction, G startValue, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (aggregateFunction == null)
            {
                throw new ArgumentNullException("Aggregate function cannot be null");
            }

            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (elementsCount == 0)
            {
                return startValue;
            }

            if (elementsCount < 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be non-negative.");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            return data.AggregateOnRangeSpanImpl(aggregateFunction, startValue, startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static G AggregateOnRangeSpanImpl<T, G>(this List<T> data, Func<G, T, G> aggregateFunction, G startValue, int startIndex, int elementsCount)
        {
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            G result = aggregateFunction(startValue, values[0]);

            for (int i = 1; i < values.Length; i++)
            {
                result = aggregateFunction(result, values[i]);
            }

            return result;
        }

        /// <summary>
        /// Finds index of the first occurrence of the given element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="element">Searched element</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the first occurrence of the given element on the given range, as for sublist where startIndex is the first index</returns>
        public static int IndexOfOnRange<T>(this List<T> data, T element, int startIndex = 0, int elementsCount = (int)Elements.All) where T : IEquatable<T>
        {
            if (element == null)
            {
                throw new ArgumentNullException("Searched element is null");
            }

            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (typeof(T) == typeof(float) && float.IsNaN((float)(object)element)
                || typeof(T) == typeof(double) && double.IsNaN((double)(object)element))
            {
                throw new ArgumentException("Searched element can't be NaN");
            }

            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be positive.");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            if (typeof(T).IsPrimitive
                && typeof(T) != typeof(float)
                && typeof(T) != typeof(double)
                && Vector.IsHardwareAccelerated
                && data.Count >= 32
                && Vector<T>.Count >= 1)
            {
                return data.IndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            }

            return IndexOfOnRangeSpanImpl(data, element, startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int IndexOfOnRangeSIMDImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
        {
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            int vectorSize = Vector<T>.Count;
            int index = 0;
            int lastChunk = values.Length - vectorSize;
            Vector<T> equalVector = new Vector<T>(element);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<T> vect = new Vector<T>(values.Slice(index, vectorSize));
                if (Vector.EqualsAny(vect, equalVector))
                {
                    for (int i = 0; i < vectorSize; i++)
                    {
                        if (vect[i].Equals(element))
                        {
                            return index + i;
                        }
                    }
                }
            }

            for (; index < values.Length; index++)
            {
                T current = values[index];
                if (values[index].Equals(element))
                {
                    return index;
                }
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int IndexOfOnRangeSpanImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
        {
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds index of the last occurrence of the given element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="element">Searched element</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the last occurrence of the given element on the given range, as for sublist where startIndex is the first index</returns>
        public static int LastIndexOfOnRange<T>(this List<T> data, T element, int startIndex = 0, int elementsCount = (int)Elements.All) where T : IEquatable<T>
        {
            if (element == null)
            {
                throw new ArgumentNullException("Searched element is null");
            }

            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (typeof(T) == typeof(float) && float.IsNaN((float)(object)element)
                || typeof(T) == typeof(double) && double.IsNaN((double)(object)element))
            {
                throw new ArgumentException("Searched element can't be NaN");
            }

            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be positive.");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            if (typeof(T).IsPrimitive
                && typeof(T) != typeof(float)
                && typeof(T) != typeof(double)
                && Vector.IsHardwareAccelerated
                && data.Count >= 32
                && Vector<T>.Count >= 1)
            {
                return data.LastIndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            }

            return LastIndexOfOnRangeSpanImpl(data, element, startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int LastIndexOfOnRangeSpanImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
        {
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            for (int i = values.Length - 1; i >= 0; i--)
            {
                if (values[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int LastIndexOfOnRangeSIMDImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
        {
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            int vectorSize = Vector<T>.Count;
            int index = values.Length - 1;
            int lastChunk = vectorSize;
            Vector<T> equalVector = new Vector<T>(element);

            for (; index >= vectorSize; index -= vectorSize)
            {
                Vector<T> vect = new Vector<T>(values.Slice(index - vectorSize + 1, vectorSize));
                if (Vector.EqualsAny(vect, equalVector))
                {
                    for (int i = vectorSize - 1; i >= 0; i--)
                    {
                        if (vect[i].Equals(element))
                        {
                            return index - vectorSize + 1 + i;
                        }
                    }
                }
            }

            for (; index >= 0; index--)
            {
                T current = values[index];
                if (values[index].Equals(element))
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds index of the smallest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the smallest element on the given range, as for sublist where startIndex is the first index</returns>
        public static int MinIndexOnRange<T>(this List<T> data, int startIndex = 0, int elementsCount = (int)Elements.All) where T : IEquatable<T>, IComparable<T>
        {
            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be positive.");
            }

            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            if (typeof(T).IsPrimitive
                && typeof(T) != typeof(float)
                && typeof(T) != typeof(double)
                && Vector.IsHardwareAccelerated
                && data.Count >= 32
                && Vector<T>.Count >= 1)
            {
                return data.MinIndexOnRangeSIMDImpl(startIndex, elementsCount);
            }

            int index = MinIndexOnRangeSpanImpl(data, startIndex, elementsCount);

            if (typeof(T) == typeof(float) && double.IsNaN((float)(object)data[index]))
            {
                throw new InvalidOperationException("Operation found index of NaN!");
            }

            if (typeof(T) == typeof(double) && double.IsNaN((double)(object)data[index]))
            {
                throw new InvalidOperationException("Operation found index of NaN!");
            }

            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int MinIndexOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IEquatable<T>, IComparable<T>
        {
            T min = data.MinOnRangeSIMDImpl(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(min, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int MinIndexOnRangeSpanImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            int index = 0;
            T min = data[startIndex];
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            for (int i = 1; i < elementsCount; i++)
            {
                T current = values[i];

                if (current is null)
                {
                    continue;
                }

                if (current.CompareTo(min) < 0)
                {
                    min = current;
                    index = i;
                }
            }

            if (min is null)
            {
                throw new InvalidOperationException("The smallest element is null");
            }

            return index;
        }

        /// <summary>
        /// Finds index of the biggest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the biggest element on the given range, as for sublist where startIndex is the first index</returns>
        public static int MaxIndexOnRange<T>(this List<T> data, int startIndex = 0, int elementsCount = (int)Elements.All) where T : IEquatable<T>, IComparable<T>
        {

            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be positive.");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            if (typeof(T).IsPrimitive
                && typeof(T) != typeof(float)
                && typeof(T) != typeof(double)
                && Vector.IsHardwareAccelerated
                && data.Count >= 32
                && Vector<T>.Count >= 1)
            {
                return data.MaxIndexOnRangeSIMDImpl(startIndex, elementsCount);
            }

            int index = MaxIndexOnRangeSpanImpl(data, startIndex, elementsCount);

            if (typeof(T) == typeof(float) && double.IsNaN((float)(object)data[index]))
            {
                throw new InvalidOperationException("Operation found index of NaN!");
            }

            if (typeof(T) == typeof(double) && double.IsNaN((double)(object)data[index]))
            {
                throw new InvalidOperationException("Operation found index of NaN!");
            }

            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int MaxIndexOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IEquatable<T>, IComparable<T>
        {
            T max = data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(max, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int MaxIndexOnRangeSpanImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            int index = 0;
            T max = data[startIndex];
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            for (int i = 1; i < elementsCount; i++)
            {
                T current = values[i];

                if (current is null)
                {
                    continue;
                }

                if (current.CompareTo(max) > 0)
                {
                    max = current;
                    index = i;
                }
            }

            if (max is null)
            {
                throw new InvalidOperationException("The biggest element is null");
            }

            return index;
        }

        /// <summary>
        /// Finds the biggest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>The biggest element on the given range</returns>
        public static T MaxOnRange<T>(this List<T> data, int startIndex = 0, int elementsCount = -1) where T : IComparable<T>
        {
            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be positive.");
            }

            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            if (typeof(T).IsPrimitive
                && typeof(T) != typeof(float)
                && typeof(T) != typeof(double)
                && Vector.IsHardwareAccelerated
                && data.Count >= 32
                && Vector<T>.Count >= 1)
            {
                return data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
            }

            T result = data.MaxOnRangeGenericImpl(startIndex, elementsCount);
            if (typeof(T) == typeof(float) && double.IsNaN((float)(object)result))
            {
                throw new InvalidOperationException("Operation result is NaN!");
            }

            if (typeof(T) == typeof(double) && double.IsNaN((double)(object)result))
            {
                throw new InvalidOperationException("Operation result is NaN!");
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T MaxOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            T max = data[startIndex];

            if (data.Count == 1)
            {
                return max;
            }

            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            int vectorSize = Vector<T>.Count;
            int index = 1;
            int lastChunk = values.Length - vectorSize;
            Vector<T> maxVector = new Vector<T>(max);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<T> vect = new Vector<T>(values.Slice(index, vectorSize));
                maxVector = Vector.Max(maxVector, vect);
            }

            for (int i = 0; i < vectorSize; i++)
            {
                T current = maxVector[i];
                if (current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }

            for (; index < values.Length; index++)
            {
                T current = values[index];
                if (current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T MaxOnRangeGenericImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            T max = data[startIndex];
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            for (int i = 1; i < values.Length; i++)
            {
                T current = values[i];
                if (current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }

            return max;
        }

        /// <summary>
        /// Finds the smallest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>The smallest element on the given range</returns>
        public static T MinOnRange<T>(this List<T> data, int startIndex = 0, int elementsCount = -1) where T : IComparable<T>
        {
            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be positive.");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            if (typeof(T).IsPrimitive
                && typeof(T) != typeof(float)
                && typeof(T) != typeof(double)
                && data.Count >= 32
                && Vector<T>.Count >= 1
                && Vector.IsHardwareAccelerated)
            {
                return data.MinOnRangeSIMDImpl(startIndex, elementsCount);
            }

            T result = data.MinOnRangeGenericImpl(startIndex, elementsCount);
            if (typeof(T) == typeof(float) && double.IsNaN((float)(object)result))
            {
                throw new InvalidOperationException("Operation result is NaN!");
            }

            if (typeof(T) == typeof(double) && double.IsNaN((double)(object)result))
            {
                throw new InvalidOperationException("Operation result is NaN!");
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T MinOnRangeGenericImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            T min = data[startIndex];
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            for (int i = 1; i < values.Length; i++)
            {
                T current = values[i];
                if (current.CompareTo(min) < 0)
                {
                    min = current;
                }
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T MinOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            T min = data[startIndex];

            if (data.Count == 1)
            {
                return min;
            }

            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            int vectorSize = Vector<T>.Count;
            int index = 1;
            int lastChunk = values.Length - vectorSize;
            Vector<T> maxVector = new Vector<T>(min);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<T> vect = new Vector<T>(values.Slice(index, vectorSize));
                maxVector = Vector.Min(maxVector, vect);
            }

            for (int i = 0; i < vectorSize; i++)
            {
                T current = maxVector[i];
                if (current.CompareTo(min) < 0)
                {
                    min = current;
                }
            }

            for (; index < values.Length; index++)
            {
                T current = values[index];
                if (current.CompareTo(min) < 0)
                {
                    min = current;
                }
            }

            return min;
        }
    }
}
