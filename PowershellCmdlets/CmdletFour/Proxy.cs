using System;
using Common;
using Habitat.Core;

namespace CmdletFour
{
    public class Proxy : MarshalByRefObject, IProxy
    {
        public string DoWork()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            string path = r.Path;
            return path;
        }
    }
}