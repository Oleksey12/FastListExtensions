namespace FastListExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using static FastListExtensions.ListRangeExtensions;

    /// <summary>
    /// Blazingly fast List<T> extensions for floating point types
    /// </summary>
    /// <remarks>
    /// <b>WARNING!</b> Use methods only when you are sure, that there is no NaN's in your data
    /// </remarks>
    public static class ListNoNanExtensions
    {
        /// <summary>
        /// Finds index of the first occurrence of the given element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="element">Searched element</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the first occurrence of the given element on the given range, as for sublist where startIndex is the first index</returns>
        public static int IndexOfOnRangeSIMD(this List<float> data, float element, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
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

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            int index = data.IndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int IndexOfOnRangeSIMDImpl(this List<float> data, float element, int startIndex, int elementsCount)
        {
            ReadOnlySpan<float> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            int vectorSize = Vector<float>.Count;
            int index = 0;
            int lastChunk = values.Length - vectorSize;
            Vector<float> equalVector = new Vector<float>(element);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<float> vect = new Vector<float>(values.Slice(index, vectorSize));
                if (Vector.EqualsAny(vect, equalVector))
                {
                    for (int i = 0; i < vectorSize; i++)
                    {
                        if (vect[i] == element)
                        {
                            return index + i;
                        }
                    }
                }
            }

            for (; index < values.Length; index++)
            {
                float current = values[index];
                if (values[index] == element)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds index of the first occurrence of the given element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="element">Searched element</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the first occurrence of the given element on the given range, as for sublist where startIndex is the first index</returns>
        public static int IndexOfOnRangeSIMD(this List<double> data, double element, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
            }

            if (elementsCount == (int)Elements.All)
            {
                elementsCount = data.Count - startIndex;
            }

            if (double.IsNaN(element))
            {
                throw new ArgumentException("Searched element can't be NaN");
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentOutOfRangeException("Element count must be positive.");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            int index = data.IndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int IndexOfOnRangeSIMDImpl(this List<double> data, double element, int startIndex, int elementsCount) 
        {
            ReadOnlySpan<double> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            int vectorSize = Vector<double>.Count;
            int index = 0;
            int lastChunk = values.Length - vectorSize;
            Vector<double> equalVector = new Vector<double>(element);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<double> vect = new Vector<double>(values.Slice(index, vectorSize));
                if (Vector.EqualsAny(vect, equalVector))
                {
                    for (int i = 0; i < vectorSize; i++)
                    {
                        if (vect[i] == element)
                        {
                            return index + i;
                        }
                    }
                }
            }

            for (; index < values.Length; index++)
            {
                double current = values[index];
                if (values[index] == element)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds index of the last occurrence of the given element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="element">Searched element</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the last occurrence of the given element on the given range, as for sublist where startIndex is the first index</returns>
        public static int LastIndexOfOnRangeSIMD(this List<float> data, float element, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
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

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            int index = data.LastIndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int LastIndexOfOnRangeSIMDImpl(this List<float> data, float element, int startIndex, int elementsCount)
        {
            ReadOnlySpan<float> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            int vectorSize = Vector<float>.Count;
            int index = values.Length - 1;
            int lastChunk = vectorSize;
            Vector<float> equalVector = new Vector<float>(element);

            for (; index >= vectorSize; index -= vectorSize)
            {
                Vector<float> vect = new Vector<float>(values.Slice(index - vectorSize + 1, vectorSize));
                if (Vector.EqualsAny(vect, equalVector))
                {
                    for (int i = vectorSize - 1; i >= 0; i--)
                    {
                        if (vect[i] == element)
                        {
                            return index - vectorSize + 1 + i;
                        }
                    }
                }
            }

            for (; index >= 0; index--)
            {
                float current = values[index];
                if (values[index] == element)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds index of the last occurrence of the given element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="element">Searched element</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the last occurrence of the given element on the given range, as for sublist where startIndex is the first index</returns>
        public static int LastIndexOfOnRangeSIMD(this List<double> data, double element, int startIndex = 0, int elementsCount = (int)Elements.All)
        {
            if (data is null || data.Count == 0)
            {
                throw new ArgumentNullException("The input list is empty");
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

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            int index = data.LastIndexOfOnRangeSIMDImpl(element, startIndex, elementsCount);
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int LastIndexOfOnRangeSIMDImpl(this List<double> data, double element, int startIndex, int elementsCount)
        {
            ReadOnlySpan<double> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);

            int vectorSize = Vector<double>.Count;
            int index = values.Length - 1;
            int lastChunk = vectorSize;
            Vector<double> equalVector = new Vector<double>(element);

            for (; index >= vectorSize; index -= vectorSize)
            {
                Vector<double> vect = new Vector<double>(values.Slice(index - vectorSize + 1, vectorSize));
                if (Vector.EqualsAny(vect, equalVector))
                {
                    for (int i = vectorSize - 1; i >= 0; i--)
                    {
                        if (vect[i] == element)
                        {
                            return index - vectorSize + 1 + i;
                        }
                    }
                }
            }

            for (; index >= 0; index--)
            {
                double current = values[index];
                if (values[index] == element)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Finds index of the biggest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the biggest element on the given range, as for sublist where startIndex is the first index</returns>
        public static int MaxIndexOnRangeSIMD(this List<float> data, int startIndex = 0, int elementsCount = (int)Elements.All)
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

            float max = data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(max, startIndex, elementsCount);
            return index;
        }

        /// <summary>
        /// Finds index of the biggest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the biggest element on the given range, as for sublist where startIndex is the first index</returns>
        public static int MaxIndexOnRangeSIMD(this List<double> data, int startIndex = 0, int elementsCount = (int)Elements.All)
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

            double max = data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(max, startIndex, elementsCount);
            return index;
        }

        /// <summary>
        /// Finds index of the smallest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the smallest element on the given range, as for sublist where startIndex is the first index</returns>
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
                throw new ArgumentNullException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            float min = data.MinOnRangeSIMDImpl(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(min, startIndex, elementsCount);
            return index;
        }

        /// <summary>
        /// Finds index of the smallest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>Index of the smallest element on the given range, as for sublist where startIndex is the first index</returns>
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
                throw new ArgumentNullException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            double min = data.MinOnRangeSIMDImpl(startIndex, elementsCount);
            int index = data.IndexOfOnRangeSIMDImpl(min, startIndex, elementsCount);
            return index;
        }

        /// <summary>
        /// Finds the biggest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>The biggest element on the given range</returns>
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
                throw new ArgumentNullException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            return data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double MaxOnRangeSIMDImpl(this List<double> data, int startIndex, int elementsCount)
        {
            double max = data[startIndex];

            if (data.Count == 1)
            {
                return max;
            }

            ReadOnlySpan<double> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            int vectorSize = Vector<double>.Count;
            int index = 1;
            int lastChunk = values.Length - vectorSize;
            Vector<double> maxVector = new Vector<double>(max);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<double> vect = new Vector<double>(values.Slice(index, vectorSize));
                maxVector = Vector.Max(maxVector, vect);
            }

            for (int i = 0; i < vectorSize; i++)
            {
                double current = maxVector[i];
                if (current > max)
                {
                    max = current;
                }
            }

            for (; index < values.Length; index++)
            {
                double current = values[index];
                if (current > max)
                {
                    max = current;
                }
            }

            return max;
        }

        /// <summary>
        /// Finds the biggest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>The biggest element on the given range</returns>
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
                throw new ArgumentNullException("The input list is empty");
            }

            if (startIndex < 0 || startIndex >= data.Count || startIndex + elementsCount > data.Count)
            {
                throw new ArgumentOutOfRangeException("Input range exceeds the list size");
            }

            return data.MaxOnRangeSIMDImpl(startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float MaxOnRangeSIMDImpl(this List<float> data, int startIndex, int elementsCount)
        {
            float max = data[startIndex];

            if (data.Count == 1)
            {
                return max;
            }

            ReadOnlySpan<float> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            int vectorSize = Vector<float>.Count;
            int index = 1;
            int lastChunk = values.Length - vectorSize;
            Vector<float> maxVector = new Vector<float>(max);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<float> vect = new Vector<float>(values.Slice(index, vectorSize));
                maxVector = Vector.Max(maxVector, vect);
            }

            for (int i = 0; i < vectorSize; i++)
            {
                float current = maxVector[i];
                if (current > max)
                {
                    max = current;
                }
            }

            for (; index < values.Length; index++)
            {
                float current = values[index];
                if (current > max)
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
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>The smallest element on the given range</returns>
        public static double MinOnRangeSIMD(this List<double> data, int startIndex = 0, int elementsCount = -1)
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

            return data.MinOnRangeSIMDImpl(startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double MinOnRangeSIMDImpl(this List<double> data, int startIndex, int elementsCount)
        {
            double min = data[startIndex];

            if (data.Count == 1)
            {
                return min;
            }

            ReadOnlySpan<double> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            int vectorSize = Vector<double>.Count;
            int index = 1;
            int lastChunk = values.Length - vectorSize;
            Vector<double> maxVector = new Vector<double>(min);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<double> vect = new Vector<double>(values.Slice(index, vectorSize));
                maxVector = Vector.Min(maxVector, vect);
            }

            for (int i = 0; i < vectorSize; i++)
            {
                double current = maxVector[i];
                if (current < min)
                {
                    min = current;
                }
            }

            for (; index < values.Length; index++)
            {
                double current = values[index];
                if (current < min)
                {
                    min = current;
                }
            }

            return min;
        }

        /// <summary>
        /// Finds the smallest element on the given range
        /// </summary>
        /// <remarks>
        /// <b>WARNING!</b> Use method only when you are sure, that there is no NaN's in your data <br><br></br></br>
        /// <b>WARNING!</b> This method is not thread-safe, it utilizes <see cref="CollectionsMarshal.AsSpan{T}(List{T}?)"/> under the hood
        /// </remarks>
        /// <param name="data">Target list</param>
        /// <param name="startIndex">Start index of the range</param>
        /// <param name="elementsCount">The number of the following elements to be taken</param>
        /// <returns>The smallest element on the given range</returns>
        public static float MinOnRangeSIMD(this List<float> data, int startIndex = 0, int elementsCount = -1)
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

            return data.MinOnRangeSIMDImpl(startIndex, elementsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float MinOnRangeSIMDImpl(this List<float> data, int startIndex, int elementsCount)
        {
            float min = data[startIndex];

            if (data.Count == 1)
            {
                return min;
            }

            ReadOnlySpan<float> values = CollectionsMarshal.AsSpan(data).Slice(startIndex, elementsCount);
            int vectorSize = Vector<float>.Count;
            int index = 1;
            int lastChunk = values.Length - vectorSize;
            Vector<float> maxVector = new Vector<float>(min);

            for (; index <= lastChunk; index += vectorSize)
            {
                Vector<float> vect = new Vector<float>(values.Slice(index, vectorSize));
                maxVector = Vector.Min(maxVector, vect);
            }

            for (int i = 0; i < vectorSize; i++)
            {
                float current = maxVector[i];
                if (current < min)
                {
                    min = current;
                }
            }

            for (; index < values.Length; index++)
            {
                float current = values[index];
                if (current < min)
                {
                    min = current;
                }
            }

            return min;
        }
    }
}
