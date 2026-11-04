using System;
using System.Reflection;
using System.Threading.Tasks;

// What NUnit does to decide whether a test method is async
// (CSharpPatternBasedAwaitAdapter.AwaitShapeInfo.TryCreate).
Type awaiter = typeof(Task).GetMethod("GetAwaiter")!.ReturnType;
Type? byName = awaiter.GetInterface("System.Runtime.CompilerServices.INotifyCompletion");
MethodInfo? onCompleted = byName?.GetMethod("OnCompleted", new[] { typeof(Action) });

Console.WriteLine($"awaiter:     {awaiter}");
Console.WriteLine($"interface:   {byName?.ToString() ?? "null"}");
Console.WriteLine($"OnCompleted: {onCompleted?.ToString() ?? "null"}");
