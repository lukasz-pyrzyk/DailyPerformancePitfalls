1. Swapping variable
``` ini

BenchmarkDotNet=v0.11.1.806-nightly, OS=Windows 10.0.16299.611 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2648436 Hz, Resolution=377.5813 ns, Timer=TSC
.NET Core SDK=2.1.403
  [Host]     : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT


```
|  Method | x | y |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|-------- |-- |-- |----------:|----------:|----------:|----------:|------:|--------:|
|    Swap | 0 | 1 | 0.0006 ns | 0.0021 ns | 0.0020 ns | 0.0000 ns |     ? |       ? |
| SwapXOR | 0 | 1 | 3.0023 ns | 0.0247 ns | 0.0231 ns | 2.9905 ns |     ? |       ? |


```assembly
; DPF.Benchmark.SwappingBenchmark.Swap(Int32 ByRef, Int32 ByRef)
       mov     eax,dword ptr [rdx]
       mov     ecx,dword ptr [r8]
       mov     dword ptr [rdx],ecx
       mov     dword ptr [r8],eax
       ret
; Total bytes of code 11
```

```assembly
; DPF.Benchmark.SwappingBenchmark.SwapXOR(Int32 ByRef, Int32 ByRef)
       mov     eax,dword ptr [r8]
       xor     dword ptr [rdx],eax
       mov     eax,dword ptr [rdx]
       xor     dword ptr [r8],eax
       mov     eax,dword ptr [r8]
       xor     dword ptr [rdx],eax
       ret
; Total bytes of code 16
```
