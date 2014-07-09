using System;
using System.Reflection;
using Common;

namespace TestConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        public void Run()
        {
            // load fake cmdlet 1 using LoadFrom
            //const string path1 = @"D:\Users\Ben\Documents\GitHub\random-detritus\PowershellCmdlets\TestModule\bin\Debug\TestModule.dll";
/*            Assembly cmdlet1Assembly = Assembly.LoadFrom(path1);
            Type cmdlet1Type = cmdlet1Assembly.GetType("TestModule.FakeCmdlet");
            MethodInfo cmdlet1Method = cmdlet1Type.GetMethod("ProcessRecord");
            object cmdlet1Instance = Activator.CreateInstance(cmdlet1Type, null);
            cmdlet1Method.Invoke(cmdlet1Instance, null);*/

            const string path1 = @"D:\Users\Ben\Documents\GitHub\random-detritus\PowershellCmdlets\CmdletThree\bin\Debug";
            ExecutionSandbox<Proxy1> sandbox1 = new ExecutionSandbox<Proxy1>(path1);
            Proxy1 proxy = sandbox1.Value;
            string output1 = proxy.InvokeCmdlet();
            sandbox1.Dispose();
            Console.Out.WriteLine(string.Format("Path is {0}", output1));

            const string path2 = @"D:\Users\Ben\Documents\GitHub\random-detritus\PowershellCmdlets\CmdletFour\bin\Debug";
            ExecutionSandbox<Proxy2> sandbox2 = new ExecutionSandbox<Proxy2>(path2);
            Proxy2 proxy2 = sandbox2.Value;
            string output2 = proxy2.InvokeCmdlet();
            sandbox2.Dispose();
            Console.Out.WriteLine(string.Format("Path is {0}", output2));
        }
    }
}
