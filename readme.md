1. Swapping variable
``` ini

BenchmarkDotNet=v0.11.1.806-nightly, OS=Windows 10.0.16299.611 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i5-7200U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2648435 Hz, Resolution=377.5815 ns, Timer=TSC
.NET Core SDK=2.1.403
  [Host]     : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT


```
|  Method | x | y |      Mean |     Error |    StdDev |  Ratio | RatioSD |
|-------- |-- |-- |----------:|----------:|----------:|-------:|--------:|
|    Swap | 0 | 1 | 0.0292 ns | 0.0090 ns | 0.0085 ns |   1.00 |    0.00 |
| SwapXOR | 0 | 1 | 2.8936 ns | 0.0054 ns | 0.0045 ns | 110.29 |   42.25 |
```assembly
; DPF.Benchmark._1_Swap_Benchmark.Swap(Int32 ByRef, Int32 ByRef)
       mov     eax,dword ptr [rdx]
       mov     ecx,dword ptr [r8]
       mov     dword ptr [rdx],ecx
       mov     dword ptr [r8],eax
       ret
; Total bytes of code 11
```
```assembly
; DPF._1_Swap.SwapXOR(Int32 ByRef, Int32 ByRef)
       mov     eax,dword ptr [rdx]
       xor     dword ptr [rcx],eax
       mov     eax,dword ptr [rcx]
       xor     dword ptr [rdx],eax
       mov     eax,dword ptr [rdx]
       xor     dword ptr [rcx],eax
       ret
; Total bytes of code 13
```