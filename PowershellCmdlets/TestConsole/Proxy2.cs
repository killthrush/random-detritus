﻿using System;
using TestModule2;

namespace TestConsole
{
    public class Proxy2 : MarshalByRefObject
    {
        public string InvokeCmdlet()
        {
            Proxy p = new Proxy();
            var t = p.DoWork();
            return t;
        }
    }
}
