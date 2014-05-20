using System;
using Habitat.Core;

namespace TestConsole
{
    public class Proxy2 : MarshalByRefObject
    {
        public string DoWork()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            string path = r.Path;
            return path;
        }
    }
}