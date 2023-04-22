using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Issue4298
{
    internal class Issue2851
    {
#if NET472_OR_GREATER || NETCOREAPP1_0_OR_GREATER

        public class SomeClass
        {
            private event Action MyEvent;

            public SomeClass()
            {
                MyEvent += OnMyEvent;
            }

            private async void OnMyEvent()
            {
                throw new NotImplementedException();
            }

            public void Fire()
            {
                MyEvent();
            }
        }

        public class Tests
        {
            [Test, SingleThreadedWithAsyncVoidWait]
            public async Task Foo()
            {
                var foo = new SomeClass();

                foo.Fire();

                Assert.True(true);
            }
        }


        public class MyTests
        {
            [Test, SingleThreadedWithAsyncVoidWait]
            public void Foo()
            {
                _ = X();

                async Task X()
                {
                    await Task.Delay(1000);
                    Y();
                }

                async void Y()
                {
                    await Task.Delay(1000);
                    Assert.Pass("Waited");
                }
            }
        }

        [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
        public sealed class SingleThreadedWithAsyncVoidWait : Attribute, ITestAction
        {
            public void BeforeTest(ITest test)
            {
                SynchronizationContext.SetSynchronizationContext(new SingleThreadedSynchronizationContext());
            }

            public void AfterTest(ITest test)
            {
                var context = SynchronizationContext.Current as SingleThreadedSynchronizationContext
                    ?? throw new InvalidOperationException("The test changed the synchronization context.");

                context.Run();

                SynchronizationContext.SetSynchronizationContext(null);
            }

            public ActionTargets Targets => ActionTargets.Default;
        }

        public sealed class SingleThreadedSynchronizationContext : SynchronizationContext
        {
            private readonly BlockingCollection<(SendOrPostCallback d, object state)> queue = new BlockingCollection<(SendOrPostCallback, object)>();

            public override void Post(SendOrPostCallback d, object state)
            {
                queue.Add((d, state));
            }

            public void Run()
            {
                while (queue.TryTake(out var work, Timeout.Infinite))
                    work.d.Invoke(work.state);
            }
        }
#endif
    }
}


