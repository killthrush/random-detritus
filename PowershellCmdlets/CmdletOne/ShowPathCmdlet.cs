using System.Management.Automation;
using Habitat.Core;

namespace CmdletOne
{
    [Cmdlet(VerbsCommon.Show, "Path")]
    public class ShowPathCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            WriteObject("Path is baz");
        }
    }
}
