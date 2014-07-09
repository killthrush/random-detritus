using System;
using TestModule;

namespace TestConsole
{
    public class Proxy1 : MarshalByRefObject
    {
        public string InvokeCmdlet()
        {
            Proxy p = new Proxy();
            string t = p.DoWork();
            return t;
        }
    }
}
