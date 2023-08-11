

namespace Issue4455;

//[MeansImplicitUse]

public class NewDatapointSourceAttribute :  DatapointSourceAttribute
{}

[TestFixture]
public class FooTests
{
    [DatapointSource] public string[] StringValues = { "some", "string", "values" };

    [NewDatapointSource] public int[] IntValues = { 0, 1, 2 };
}