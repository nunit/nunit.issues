using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issue4549;

public class Issue3117
{
    [TestCase(1, TestName = "{c}.{m} Flag 1, i.e. H=(h11 & 1 \\ -1 & h22")]
    [TestCase(-2, TestName = "{c}.{m} Flag -2, i.e. H=(1 & 0 \\ 0 & 1)")]
    public void drotm_TestCaseData_ResultOfManuallyBechmark(int flag)
    {
    }
}