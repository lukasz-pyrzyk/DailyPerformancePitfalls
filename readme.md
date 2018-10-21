### Environment
``` ini
BenchmarkDotNet=v0.11.1.806-nightly, OS=Windows 10.0.16299.611 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2648436 Hz, Resolution=377.5813 ns, Timer=TSC
.NET Core SDK=2.1.403
  [Host]     : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
```

### 1. Swapping variable

|  Method | x | y |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|-------- |-- |-- |----------:|----------:|----------:|----------:|------:|--------:|
|    Swap | 0 | 1 | 0.0006 ns | 0.0021 ns | 0.0020 ns | 0.0000 ns |     ? |       ? |
| SwapXOR | 0 | 1 | 3.0023 ns | 0.0247 ns | 0.0231 ns | 2.9905 ns |     ? |       ? |

### 2. Counting even numbers
|            Method |       Mean |     Error |    StdDev | Ratio | RatioSD | BranchMispredictions/Op |
|------------------ |-----------:|----------:|----------:|------:|--------:|------------------------:|
| PartitionerCreate |   5.169 ms | 0.0221 ms | 0.0206 ms |  0.34 |    0.00 |                   1,799 |
|               For |  15.163 ms | 0.0543 ms | 0.0508 ms |  1.00 |    0.00 |                   1,227 |
|  PLINQ_AsParallel |  36.869 ms | 0.2220 ms | 0.2076 ms |  2.43 |    0.02 |                 102,521 |
|   ParallelForEach | 164.636 ms | 3.2173 ms | 4.4039 ms | 10.76 |    0.36 |                  65,087 |

### 3. Structure parsing

|                  Method |       Mean |      Error |    StdDev | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |-----------:|-----------:|----------:|------------:|------------:|------------:|--------------------:|
|                 Pointer |   1.063 ns |  0.0305 ns | 0.0285 ns |           - |           - |           - |                   - |
|        ManualWithUnsafe |  21.815 ns |  0.2084 ns | 0.1949 ns |           - |           - |           - |                   - |
|                  Manual |  23.029 ns |  0.3171 ns | 0.2966 ns |           - |           - |           - |                   - |
|    PtrToStructure_Fixed | 192.979 ns |  2.1008 ns | 1.9651 ns |      0.0253 |           - |           - |                40 B |
| PtrToStructure_GCHandle | 276.896 ns |  2.5303 ns | 2.3668 ns |      0.0253 |           - |           - |                40 B |
|              Reflection | 878.013 ns | 10.2428 ns | 9.5811 ns |      0.1621 |           - |           - |               256 B |

### 4. Inlining
|                    Method | a | b |      Mean |     Error |    StdDev | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |-- |-- |----------:|----------:|----------:|------------:|------------:|------------:|--------------------:|
|                    Divide | 0 | 1 | 0.0000 ns | 0.0000 ns | 0.0000 ns |           - |           - |           - |                   - |
|             DivideVirtual | 0 | 1 | 0.6666 ns | 0.0335 ns | 0.0297 ns |           - |           - |           - |                   - |
|       DivideWithException | 0 | 1 | 1.2798 ns | 0.0295 ns | 0.0276 ns |           - |           - |           - |                   - |
| DivideWithExceptionHelper | 0 | 1 | 0.9894 ns | 0.0475 ns | 0.0421 ns |           - |           - |           - |                   - |

### 5. Fibonacci
|        Method |  n |              Mean |           Error |          StdDev |        Ratio |   RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------- |--- |------------------:|----------------:|----------------:|-------------:|----------:|------------:|------------:|------------:|--------------------:|
|     **Recursive** | **15** |      **3,002.064 ns** |     **145.9242 ns** |     **430.2608 ns** |       **369.62** |     **25.03** |           **-** |           **-** |           **-** |                   **-** |
|  RecursiveTpl | 15 |      6,475.247 ns |     136.8563 ns |     232.3923 ns |       733.52 |     34.71 |      0.2899 |           - |           - |               464 B |
| RecursiveMemo | 15 |         72.324 ns |       0.8102 ns |       0.7182 ns |         8.10 |      0.15 |      0.0914 |           - |           - |               144 B |
|     Iterative | 15 |          8.933 ns |       0.1157 ns |       0.1082 ns |         1.00 |      0.00 |           - |           - |           - |                   - |
|               |    |                   |                 |                 |              |           |             |             |             |                     |
|     **Recursive** | **35** | **38,112,373.096 ns** | **141,724.7278 ns** | **132,569.3983 ns** | **1,703,863.64** | **21,205.17** |           **-** |           **-** |           **-** |                   **-** |
|  RecursiveTpl | 35 | 32,011,899.515 ns | 631,642.8714 ns | 702,069.3779 ns | 1,432,469.88 | 31,929.34 |           - |           - |           - |               488 B |
| RecursiveMemo | 35 |        190.665 ns |       1.8157 ns |       1.6984 ns |         8.52 |      0.12 |      0.1931 |           - |           - |               304 B |
|     Iterative | 35 |         22.371 ns |       0.2494 ns |       0.2332 ns |         1.00 |      0.00 |           - |           - |           - |                   - |
