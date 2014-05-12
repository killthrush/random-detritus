using System.Management.Automation;
using Habitat.Core;

namespace CmdletTwo
{
    [Cmdlet(VerbsCommon.Show, "Path2")]
    public class ShowPath2Cmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            string path = r.Path;
            WriteObject(string.Format("Path is {0}", path));
        }
    }
}
