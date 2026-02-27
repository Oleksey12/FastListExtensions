namespace TestListExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { 1, 2, 3 };
            List<int> list = new List<int> { 1, 2, 3 };
            Span<int> span = CollectionsMarshal.AsSpan(list);


        }
    }
}