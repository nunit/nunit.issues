using NUnit.Framework;
using System.IO;
using System;

namespace Issue4298
{
    internal class Issue4353
    {
        private string _path;
        private string _tempDirectory;

        [SetUp]
        public void Setup()
        {
            _tempDirectory = Environment.ExpandEnvironmentVariables(@"%TEMP%\NUnitTest4353");

            Directory.CreateDirectory(_tempDirectory);

            var fileNameLength = (260 - _tempDirectory.Length) + 3;
            var file = new string('A', fileNameLength) + ".txt";

            _path = Path.Combine(_tempDirectory, file);

#if NETFRAMEWORK
            // Work around for long paths on netfx. Commented out to induce failure
            //if (_path.Length > 260 && Path.IsPathRooted(_path))
            //    _path = $@"\\?\{_path}";
#endif

            File.WriteAllText(_path, "Hello World");
        }

        [Test]
        public void Test1()
        {
            TestContext.AddTestAttachment(_path); //throws exception when path is too long
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_path);
            Directory.Delete(_tempDirectory);
        }
    }
}


