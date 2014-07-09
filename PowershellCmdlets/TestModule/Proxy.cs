using System;
using Habitat.Core;

namespace TestModule
{
    [Serializable]
    public class Proxy : MarshalByRefObject
    {

        public string DoWork()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            var e = r.Entities;
            return "baz";
        }
    }
}