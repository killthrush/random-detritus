using System.IO;
using System.Management.Automation;
using System.Reflection;
using Common;

namespace CmdletThree
{
    [Cmdlet(VerbsCommon.Show, "Path3")]
    public class ShowPathCmdlet3 : Cmdlet
    {
        protected override void ProcessRecord()
        {
            string cmdletExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ExecutionSandbox<Proxy> executionSandbox = new ExecutionSandbox<Proxy>(cmdletExecutionPath);
            Proxy proxy = executionSandbox.Value;
            string path = proxy.DoWork();
            executionSandbox.Dispose();
            WriteObject(string.Format("Path is {0}", path));
        }
    }
}
