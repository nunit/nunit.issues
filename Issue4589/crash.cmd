dotnet test -c Release -s .runsettings --filter "TestCategory!=Sample&TestCategory!=Stress" --logger "Console;verbosity=normal"

PAUSE