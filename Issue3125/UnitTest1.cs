namespace Issue3125;

class Test
   {
      private static readonly TestCaseData[] Cases =
      {
         new TestCaseData("", 0),
         new TestCaseData("", 1)
      };
      
      [TestCaseSource(nameof(Cases))]
      public void TestA(string a, float b)
      {
      }

      [TestCaseSource(nameof(Cases))]
      public void TestB(string a, int b)
      {
      }
   }