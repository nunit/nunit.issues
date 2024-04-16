using InvertedTomato.Crc;
using NUnit.Framework;
using System.Text;
using System;

public class Tests
{
    [Test]
    public void BugReport()
    {
        var crc = CrcAlgorithm.CreateCrc16CcittFalse();

        // Give it some bytes to chew on - you can call this multiple times if needed
        crc.Append(Encoding.ASCII.GetBytes("Hurray for cake!"));

        // Get the output - as a hex string, byte array or unsigned integer
        Console.WriteLine(crc.ToHexString());
    }
}