using System;
using Common;
using Habitat.Core;

namespace CmdletThree
{
    public class Proxy : MarshalByRefObject, IProxy
    {
        public string DoWork()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            var e = r.Entities;
            return "baz";
        }
    }
}