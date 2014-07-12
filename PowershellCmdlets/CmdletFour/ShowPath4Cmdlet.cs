using System.IO;
using System.Management.Automation;
using System.Reflection;
using Common;

namespace CmdletFour
{
    [Cmdlet(VerbsCommon.Show, "Path4")]
    public class ShowPath4Cmdlet : Cmdlet
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
