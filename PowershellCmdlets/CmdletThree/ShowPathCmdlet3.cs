using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using Common;

namespace CmdletThree
{
    [Cmdlet(VerbsCommon.Show, "Path3")]
    public class ShowPathCmdlet3 : Cmdlet
    {
        private static ExecutionSandbox<Proxy> _executionSandbox;
        private readonly object _lockObject = new object();

        protected override void ProcessRecord()
        {
            DateTime start = DateTime.Now;

            lock (_lockObject)
            {
                if (_executionSandbox == null)
                {
                    string cmdletExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    _executionSandbox = new ExecutionSandbox<Proxy>(cmdletExecutionPath);
                }
            }

            Proxy proxy = _executionSandbox.ObjectProxy;
            string path = proxy.DoWork();

            DateTime end = DateTime.Now;

            WriteObject(string.Format("Value is {0}.  Elapsed MS: {1}", path, (end - start).TotalMilliseconds));
        }
    }
}
