using System;
using System.IO;
using System.Reflection;
using Common;

namespace TestModule
{
    public class FakeCmdlet
    {
        public void ProcessRecord()
        {
            string p = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ExecutionSandbox<Proxy> executionSandbox = new ExecutionSandbox<Proxy>(p);
            Proxy proxy = executionSandbox.Value;
            string path = proxy.DoWork();
            executionSandbox.Dispose();
            Console.Out.WriteLine(string.Format("Path is {0}", path));
        }
    }
}
