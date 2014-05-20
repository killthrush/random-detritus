using System;
using System.IO;
using System.Reflection;
using Common;

namespace TestConsole
{
    public class Program
    {
        [LoaderOptimization(LoaderOptimization.MultiDomain)]
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        public void Run()
        {
            string path1 = @"D:\Users\Ben\Documents\GitHub\random-detritus\PowershellCmdlets\CmdletThree\bin\Debug";
            ExecutionSandbox<Proxy> sandbox1 = new ExecutionSandbox<Proxy>(path1);
            Proxy proxy = sandbox1.Value;
            string output1 = proxy.DoWork();
            sandbox1.Dispose();
            Console.Out.WriteLine(string.Format("Path is {0}", output1));

            string path2 = @"D:\Users\Ben\Documents\GitHub\random-detritus\PowershellCmdlets\CmdletFour\bin\Debug";
            ExecutionSandbox<Proxy2> sandbox2 = new ExecutionSandbox<Proxy2>(path2);
            Proxy2 proxy2 = sandbox2.Value;
            string output2 = proxy2.DoWork();
            sandbox2.Dispose();
            Console.Out.WriteLine(string.Format("Path is {0}", output2));
        }
    }
}
