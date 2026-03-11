# FastListExtensions

> Performant extensions for List&lt;T&gt;

### License
MIT. You are free to use, modify, and distribute the project without any charge.

### Summary
The project contains different useful methods that would help develop your logic without making duplications.

All developed methods are well-tested, allocation free and utilizes most performant features (like SIMD operations and ReadOnlySpan&lt;T&gt;)

### Development pipeline

Our team selected the following pipeline for creating reliable extensions:

1. Method analysis (arguments, algorithms, edge cases)
2. Writing test implementations
3. Benchmarking performance with Benchmark .NET
4. Results analysis and creating final implementations
5. Writing unit tests

### Ready to use extensions

- ListBasicExtensions (Pop, PopFirst, TryFind and e.t.c ) - simple extension methods
- ListRangeExtensions (ForEachOnRange, AggregateOnRange, MaxIndexOnRange and e.t.c ) - extensions for performant operations with range support
- ListNoNanExtensions (MaxIndexOnRangeSIMD, MaxRangeSIMD and e.t.c) - blazingly fast extensions for floating point types
- TODO: ListSelectExtensions 
- TODO: ListSelectConvertExtensions 

### Documentation

```csharp
List<int> list = new List<int> { 1, 2, 3, 4, 5, 6 };
int value = list.Pop(); // Removes and returns last element - 6, now the list is 1, 2, 3, 4, 5 

if (!list.TryFind(x => x > 2, out int result)) // Method will find 3 and result will be equal to 3
{
    return;
}

int biggest = list.MaxIndexOnRange(2); // Will find index of the biggest element, it will be 2
```

TODO: Create proper documentation

### Benchmarks

TODO: Present the results
