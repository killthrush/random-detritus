using System;
using Habitat.Core;

namespace CmdletFour
{
    /// <summary>
    /// This class encapsulates logic that needs to run inside a powershell cmdlet, but isolated from other cmdlets.
    /// </summary>
    public class Proxy : MarshalByRefObject, IProxy
    {
        /// <summary>
        /// Do some *real* work
        /// </summary>
        /// <returns>A bit of data to export to the console</returns>
        public string DoWork()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            string path = r.Path;
            return path;
        }
    }
}