namespace ListExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class TestLe
    {
        private enum Elements
        {
            All = -1
        }

        #region IndexOfOnRange

        public static int IndexOfOnRange<T>(this List<T> data, T element, int startIndex = 0, int elementsCount = (int)Elements.All) where T : IEquatable<T>
        {
            if (element == null)
            {
                throw new ArgumentNullException("Searched element is null");
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
                && Vector.IsHardwareAccelerated
                && data.Count >= 32
                && Vector<T>.Count >= 1)
            {
                return data.IndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            }

            return IndexOfOnRangeSpanImpl(data, element, startIndex, elementsCount);
        }

        public static int IndexOfOnRangeSIMD(this List<float> data, float element, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Searched element is null");
            }

            if (float.IsNaN(element))
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

            if (data is null || data.Count == 0)
            {
                throw new ArgumentException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            int index = data.IndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            return index;
        }

        public static int IndexOfOnRangeSIMD(this List<double> data, double element, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Searched element is null");
            }

            if (double.IsNaN(element))
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

            if (data is null || data.Count == 0)
            {
                throw new ArgumentException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            int index = data.IndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfOnRangeSpanImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfOnRangeListImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
        {
            for (int i = startIndex; i < elementsCount; i++)
            {
                if (data[i].Equals(element))
                {
                    return i - startIndex;
                }
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfOnRangeSIMDImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
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

        #endregion

        #region LastIndexOfOnRange

        public static int LastIndexOfOnRange<T>(this List<T> data, T element, int startIndex = 0, int elementsCount = (int)Elements.All) where T : IEquatable<T>
        {
            if (element == null)
            {
                throw new ArgumentNullException("Searched element is null");
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
                && Vector.IsHardwareAccelerated
                && data.Count >= 32
                && Vector<T>.Count >= 1)
            {
                return data.LastIndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            }

            return LastIndexOfOnRangeSpanImpl(data, element, startIndex, elementsCount);
        }

        public static int LastIndexOfOnRangeSIMD(this List<float> data, float element, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Searched element is null");
            }

            if (float.IsNaN(element))
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

            if (data is null || data.Count == 0)
            {
                throw new ArgumentException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            int index = data.LastIndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            return index;
        }

        public static int LastIndexOfOnRangeSIMD(this List<double> data, double element, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Searched element is null");
            }

            if (double.IsNaN(element))
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

            if (data is null || data.Count == 0)
            {
                throw new ArgumentException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            int index = data.LastIndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LastIndexOfOnRangeSpanImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
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
        public static int LastIndexOfOnRangeListImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
        {
            for (int i = startIndex + elementsCount - 1; i >= startIndex; i--)
            {
                if (data[i].Equals(element))
                {
                    return i - startIndex;
                }
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LastIndexOfOnRangeSIMDImpl<T>(this List<T> data, T element, int startIndex, int elementsCount) where T : IEquatable<T>
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

        #endregion

        #region MaxIndexOnRange

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
        public static int MaxIndexOnRange<T>(this List<T> data, int startIndex = 0, int elementsCount = (int)Elements.All) where T : IEquatable<T>, IComparable<T>
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
                && Vector.IsHardwareAccelerated
                && data.Count >= 32
                && Vector<T>.Count >= 1)
            {
                return data.MaxIndexOnRangeSIMDImpl(startIndex, elementsCount);
            }

            int index =  MaxIndexOnRangeSpanImpl(data, startIndex, elementsCount);
          
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

        public static int MaxIndexOnRangeSIMD(this List<float> data, int startIndex = 0, int elementsCount = (int)Elements.All)
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

            float max = data.MaxOnRangeSIMD(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(max, startIndex, elementsCount);
            return index;
        }

        public static int MaxIndexOnRangeSIMD(this List<double> data, int startIndex = 0, int elementsCount = (int)Elements.All)
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

            double max = data.MaxOnRangeSIMD(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(max, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxIndexOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IEquatable<T>, IComparable<T>
        {
            T max = data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
            int index = data.IndexOf(max, startIndex, elementsCount);
            return index;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxIndexOnRangeSpanImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxIndexOnRangeListImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            int index = startIndex;
            T max = data[startIndex];

            for (int i = startIndex + 1; i < elementsCount; i++)
            {
                T current = data[i];

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

            return index - startIndex;
        }

        #endregion

        #region MinIndexOnRange
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
                throw new ArgumentException("The input list is empty");
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

        public static int MinIndexOnRangeSIMD(this List<float> data, int startIndex = 0, int elementsCount = (int)Elements.All)
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

            float min = data.MinOnRangeSIMD(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(min, startIndex, elementsCount);
            return index;
        }

        public static int MinIndexOnRangeSIMD(this List<double> data, int startIndex = 0, int elementsCount = (int)Elements.All)
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

            double min = data.MinOnRangeSIMD(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(min, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MinIndexOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IEquatable<T>, IComparable<T>
        {
            T min = data.MinOnRangeSIMDImpl(startIndex, elementsCount);
            int index = data.IndexOf(min, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MinIndexOnRangeSpanImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
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
                throw new InvalidOperationException("The biggest element is null");
            }

            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MinIndexOnRangeListImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
        {
            int index = startIndex;
            T min = data[startIndex];

            for (int i = startIndex + 1; i < elementsCount; i++)
            {
                T current = data[i];

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
                throw new InvalidOperationException("The biggest element is null");
            }

            return index - startIndex;
        }

        #endregion

        #region MaxOnRange

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
        public static T MaxOnRangeListGenericImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
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
        public static T MaxOnRangeGenericImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
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
        public static T MaxOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
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

        #endregion

        #region MinOnRange
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
        public static T MinOnRangeGenericImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
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
        public static T MinOnRangeSIMDImpl<T>(this List<T> data, int startIndex, int elementsCount) where T : IComparable<T>
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

        #endregion

    }
}
