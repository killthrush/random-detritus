using System;
using Common;
using Habitat.Core;

namespace CmdletThree
{
    [Serializable]
    public class Proxy : MarshalByRefObject, IWorker
    {

        public string DoWork()
        {
            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            var e = r.Entities;
            return "baz";
        }
    }
}