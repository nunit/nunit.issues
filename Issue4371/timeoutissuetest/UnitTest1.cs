namespace timeoutissuetest;


public class UnitTest1
{
    [Timeout(10000)] //10s timeout
    [Test]
    public void PublicNunitTimeoutSqlCommandTest()
    {

        //any bad connection strin that respect format of System.Data.OracleClient.OracleConnection
        var badConnectionString = "DataSource=(DESCRIPTION =" +
                                  "(ADDRESS = (PROTOCOL=TCP)(HOST=urHost)(PORT=urPort)) " +
                                  "(CONNECT_DATA=(SID=urOracleSID)));" +
                                  "User Id=urUsername;Password=urPassword;  ";

        using (var connection = new System.Data.OracleClient.OracleConnection(badConnectionString))
        {
            connection.Open(); //Will take 1 mins as default timeout
            Console.WriteLine("Will never reach here");
        }
    }
}