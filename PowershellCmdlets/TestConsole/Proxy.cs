using System;
using Habitat.Core;

namespace TestConsole
{
    public class Proxy : MarshalByRefObject
    {
        public string DoWork()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            return "baz";
        }
    }
}