namespace ListExtensions
{
    using BenchmarkDotNet.Disassemblers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Intrinsics;
    using static System.MemoryExtensions;

    public static class TestLe
    {
        private enum Elements
        {
            All = -1
        }

        /// <summary>
        /// Finds the index of biggest element in the list
        /// </summary>
        /// <remarks>
        /// This method is <b>not thread-safe</b>. If the list can be modified concurrently 
        /// you must synchronize access, for example with a <see cref="lock"/>.
        /// The method uses <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood,
        /// any concurrent list modification can cause unpredicted behavior. 
        /// </remarks>
        /// <param name="data">Input list</param>
        /// <returns>Index of biggest element</returns>
        /// <exception cref="ArgumentException">Throws if the list is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if the list range is not correct</exception>
        /// <exception cref="InvalidOperationException">Throws the biggest element is null</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxIndexOnRange<T>(this List<T> data, int startIndex = 0, int elementsCount = (int)Elements.All) where T : IEquatable<T>, IComparable<T>
        {
            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count;
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be positive.");
            }

            if (data is null || data.Count == 0)
            {
                throw new ArgumentException("The input list is empty");
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
                return data.MaxIndexOnRangeSIMDImpl(startIndex, elementsCount);
            }

            return MaxIndexOnRangeSpanImpl(data, startIndex, elementsCount);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxIndexOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IEquatable<T>, IComparable<T>
        {
            T max = data.MaxOnRange(startIndex, elementsCount);
            ReadOnlySpan<T> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            int index = values.IndexOf(max);
            return startIndex + index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxIndexOnRangeSpanImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            int maxIndex = startIndex;
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
                    maxIndex = i;
                }
            }

            if (max is null)
            {
                throw new InvalidOperationException("The biggest element is null");
            }

            return startIndex + maxIndex;
        }


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
                throw new ArgumentException("The input list is empty");
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

        public static double MaxOnRangeSIMD(this List<double> data, int startIndex = 0, int elementsCount = -1)
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
                throw new ArgumentException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            return data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
        }

        public static float MaxOnRangeSIMD(this List<float> data, int startIndex = 0, int elementsCount = -1)
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
                throw new ArgumentException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            return data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOnRangeListGenericImpl<T>(this List<T> data, int startIndex, int elementsCount = -1) where T : IComparable<T>
        {
            T max = data[startIndex];
            for (int i = startIndex + 1; i < startIndex + elementsCount; i++)
            {
                T current = data[i];
                if (current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOnRangeGenericImpl<T>(this List<T> data, int startIndex, int elementsCount = -1) where T : IComparable<T>
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount = -1) where T : IComparable<T>
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


        public static T MinOnRange<T>(this List<T> data, int startIndex = 0, int elementsCount = -1) where T : IComparable<T>
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
                throw new ArgumentException("The input list is empty");
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

        public static double MinOnRangeSIMD(this List<double> data, int startIndex = 0, int elementsCount = -1)
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
                throw new ArgumentException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            return data.MinOnRangeSIMDImpl(startIndex, elementsCount);
        }

        public static float MinOnRangeSIMD(this List<float> data, int startIndex = 0, int elementsCount = -1)
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
                throw new ArgumentException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            return data.MinOnRangeSIMDImpl(startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOnRangeGenericImpl<T>(this List<T> data, int startIndex, int elementsCount = -1) where T : IComparable<T>
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
        public static T MinOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount = -1) where T : IComparable<T>
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
