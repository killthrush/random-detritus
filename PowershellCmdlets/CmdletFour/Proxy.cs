using System;
using Habitat.Core;

namespace CmdletFour
{
    public class Proxy : MarshalByRefObject
    {

        public string DoWork()
        {

            DurableMemoryRepository<string> r = new DurableMemoryRepository<string>("foobar", new FileSystemFacade());
            string path = r.Path;
            return path;
        }
    }
}