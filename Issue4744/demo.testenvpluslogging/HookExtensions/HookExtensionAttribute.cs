using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace demo.testenvpluslogging.HookExtensions;

internal class HookExtensionAttribute : NUnitAttribute, IApplyToContext
{
    private static TestEnvHookExtension TestEnvExtension;
    private static LoggingHookExtension LoggingExtension;

    public void ApplyToContext(TestExecutionContext context)
    {
        RegisterTestEnvHookExtension(context);
        RegisterLoggingHookExtension(context);
    }

    private void RegisterLoggingHookExtension(TestExecutionContext context)
    {
        LoggingExtension = new LoggingHookExtension();
        context.HookExtension.BeforeAnySetUps += LoggingExtension.BeforeAnySetUps;
        context.HookExtension.AfterAnySetUps += LoggingExtension.AfterAnySetUps;
        context.HookExtension.BeforeTest += LoggingExtension.BeforeTest;
        context.HookExtension.AfterTest += LoggingExtension.AfterTest;
        context.HookExtension.BeforeAnyTearDowns += LoggingExtension.BeforeAnyTearDowns;
        context.HookExtension.AfterAnyTearDowns += LoggingExtension.AfterAnyTearDowns;
    }

    private void RegisterTestEnvHookExtension(TestExecutionContext context)
    {
        TestEnvExtension = new TestEnvHookExtension();
        context.HookExtension.BeforeAnySetUps += TestEnvExtension.BeforeAnySetUps;
        context.HookExtension.AfterAnySetUps += TestEnvExtension.AfterAnySetUps;
        context.HookExtension.BeforeTest += TestEnvExtension.BeforeTest;
        context.HookExtension.AfterTest += TestEnvExtension.AfterTest;
        context.HookExtension.BeforeAnyTearDowns += TestEnvExtension.BeforeAnyTearDowns;
        context.HookExtension.AfterAnyTearDowns += TestEnvExtension.AfterAnyTearDowns;
    }
}