Ref PR [4413](https://github.com/nunit/nunit/pull/4430#issuecomment-1646980222)

> We should handle conditional formatting as an addition afterwards. I am not sure why we actually need it btw, but perhaps some discussion I have missed?

String formatting is a very expensive operation. Doing this for every unit tests will make tests run much slower.
I created a benchmark test quantifying this, comparing 3 cases:
* NUnit3's: format and params arguments: `UseFormatAndArguments`
* NUnit4's: string using `$""` syntax: `UsePreformattedMessage`
* PRs: FormattableString using `$""` syntax: `UseFormattableMessage`

On tests that never fail, the pre-formatted option is 6x slower (net7.0) or even 22x slower (net48).
The FormattableString is still 2x slower than the message and arguments, because it needs to allocate the FormattableString instance.
I don't know what in real world the amount of tests is that pass in extra arguments.
From our main applications 209978 Assert calls only 445 use a format string ({0}) and 370 use a interpolated string ($"").

```
|                 Method |            Runtime |       Mean | Ratio | Allocated | Alloc Ratio |
|----------------------- |------------------- |-----------:|------:|----------:|------------:|
|  UseFormatAndArguments |           .NET 7.0 |   4.306 ns |  1.00 |      56 B |        1.00 |
| UsePreformattedMessage |           .NET 7.0 |  26.917 ns |  6.25 |     104 B |        1.86 |
|  UseFormattableMessage |           .NET 7.0 |   9.198 ns |  2.14 |      88 B |        1.57 |
|                        |                    |            |       |           |             |
|  UseFormatAndArguments | .NET Framework 4.8 |   5.954 ns |  1.00 |      56 B |        1.00 |
| UsePreformattedMessage | .NET Framework 4.8 | 134.632 ns | 22.62 |     160 B |        2.86 |
|  UseFormattableMessage | .NET Framework 4.8 |  12.593 ns |  2.12 |      88 B |        1.57 |
```

If we add a default message, it comes in as follows:
```
|                 Method |            Runtime |        Mean |  Ratio | Allocated | Alloc Ratio |
|----------------------- |------------------- |------------:|-------:|----------:|------------:|
|  UseFormatAndArguments |           .NET 7.0 |   5.7493 ns |  1.000 |      56 B |        1.00 |
| UsePreformattedMessage |           .NET 7.0 |  32.9035 ns |  5.691 |     104 B |        1.86 |
|  UseFormattableMessage |           .NET 7.0 |  11.2665 ns |  1.945 |      88 B |        1.57 |
|      UseDefaultMessage |           .NET 7.0 |   0.0133 ns |  0.002 |         - |        0.00 |
|                        |                    |             |        |           |             |
|  UseFormatAndArguments | .NET Framework 4.8 |   7.2573 ns |  1.000 |      56 B |        1.00 |
| UsePreformattedMessage | .NET Framework 4.8 | 160.7722 ns | 22.178 |     160 B |        2.86 |
|  UseFormattableMessage | .NET Framework 4.8 |  13.2190 ns |  1.817 |      88 B |        1.57 |
|      UseDefaultMessage | .NET Framework 4.8 |   0.0078 ns |  0.001 |         - |        0.00 |
```

