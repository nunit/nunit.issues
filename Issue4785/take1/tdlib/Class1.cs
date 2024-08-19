using System.Diagnostics;
using System.Reflection;

namespace tdlib;

public class SomeUtilityClass
{
    public bool SomeUtilityMethod()
    {
        bool found = IsAttributeInCallStack("TearDownAttribute");

        Console.WriteLine($"SomeUtilityMethod: {found}");
        return found;
    }

    private bool IsAttributeInCallStack(string attributeName)
    {
        // Get the current call stack
        StackTrace stackTrace = new StackTrace();

        // Iterate through each frame in the stack
        foreach (StackFrame frame in stackTrace.GetFrames())
        {
            MethodBase method = frame.GetMethod();

            if (method == null)
                continue;

            // Get all custom attributes of the method
            var attributes = method.GetCustomAttributes(false);
            foreach (var attribute in attributes)
            {
                // Check if the attribute's name matches the specified attribute name
                if (attribute.GetType().Name == attributeName)
                {
                    return true; // Attribute found
                }
            }
        }

        return false; // No matching attribute found in the call stack
    }

    //private bool IsTearDownMethodInCallStack()
    //{
    //    // Get the current call stack
    //    StackTrace stackTrace = new StackTrace();

    //    // Iterate through each frame in the stack
    //    foreach (StackFrame frame in stackTrace.GetFrames())
    //    {
    //        MethodBase method = frame.GetMethod();

    //        if (method == null)
    //            continue;

    //        // Check if the method has the TearDown attribute
    //        object[] attributes = method.GetCustomAttributes(typeof(TearDownAttribute), false);
    //        if (attributes.Length > 0)
    //        {
    //            return true; // TearDownAttribute found
    //        }
    //    }

    //    return false; // No TearDownAttribute found in the call stack
    //}

}
