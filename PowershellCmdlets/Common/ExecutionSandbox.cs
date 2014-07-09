using System;
using System.IO;
using System.Reflection;

namespace Common
{
    [Serializable]
    public class ExecutionSandbox<T> : IDisposable //where T : MarshalByRefObject
    {
        public static string _probingPath;
        private AppDomain _domain;

        public ExecutionSandbox(string probingPath)
        {
            _probingPath = probingPath;
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Type sandboxedType = typeof (T);
            AppDomainSetup domainInfo = new AppDomainSetup();
            domainInfo.ApplicationBase = probingPath;
            //domainInfo.DisallowApplicationBaseProbing = true;
            //domainInfo.LoaderOptimization = LoaderOptimization.SingleDomain;
            //AppDomain.CurrentDomain.AssemblyResolve += A.MyAssemblyResolveEventHandler;
            _domain = AppDomain.CreateDomain(string.Format("Sandbox.{0}", sandboxedType.Namespace), null, domainInfo);
            //_domain.Load(GetType().Assembly.GetName());
            _domain.AssemblyResolve += A.MyAssemblyResolveEventHandler;
            _domain.AssemblyLoad += A.MyAssemblyLoadEventHandler;
            string assemblyName = sandboxedType.Assembly.FullName;

            var a = Path.Combine(probingPath, sandboxedType.Assembly.GetName().Name) + ".dll";
            //Assembly y = Assembly.LoadFrom(a);
            //_domain.Load(y.GetName());
            object instanceAndUnwrap = _domain.CreateInstanceFromAndUnwrap(/*assemblyName*//*a*/@"D:\Users\Ben\Documents\GitHub\random-detritus\PowershellCmdlets\CmdletThree\bin\Debug\CmdletThree.dll", sandboxedType.FullName/*"CmdletThree.Proxy"*/);
            Value = (T)instanceAndUnwrap;
            //Value = (T) _domain.CreateInstanceFromAndUnwrap(a, sandboxedType.FullName);
        }

        public ExecutionSandbox(string probingPath, int i)
        {
            _probingPath = probingPath;
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Type sandboxedType = typeof(T);
            AppDomainSetup domainInfo = new AppDomainSetup();
            domainInfo.ApplicationBase = probingPath;
            //domainInfo.DisallowApplicationBaseProbing = true;
            //domainInfo.LoaderOptimization = LoaderOptimization.SingleDomain;
            //AppDomain.CurrentDomain.AssemblyResolve += A.MyAssemblyResolveEventHandler;
            _domain = AppDomain.CreateDomain(string.Format("Sandbox.{0}", sandboxedType.Namespace), null, domainInfo);
            //_domain.Load(GetType().Assembly.GetName());
            _domain.AssemblyResolve += A.MyAssemblyResolveEventHandler;
            _domain.AssemblyLoad += A.MyAssemblyLoadEventHandler;
            string assemblyName = sandboxedType.Assembly.FullName;

            var a = Path.Combine(probingPath, sandboxedType.Assembly.GetName().Name) + ".dll";
            //Assembly y = Assembly.LoadFrom(a);
            //_domain.Load(y.GetName());
            object instanceAndUnwrap = _domain.CreateInstanceFromAndUnwrap(/*assemblyName*//*a*/@"D:\Users\Ben\Documents\GitHub\random-detritus\PowershellCmdlets\CmdletFour\bin\Debug\CmdletFour.dll", sandboxedType.FullName/*"CmdletFour.Proxy"*/);
            Value = (T)instanceAndUnwrap;
            //Value = (T) _domain.CreateInstanceFromAndUnwrap(a, sandboxedType.FullName);
        }

        public T Value { get; private set; }

        public void Dispose()
        {
            if (_domain != null)
            {
                AppDomain.Unload(_domain);
                _domain = null;
            }
        }
    }

    public class A : MarshalByRefObject
    {
        public static void MyAssemblyLoadEventHandler(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("ASSEMBLY LOADED: " + args.LoadedAssembly.FullName);
            Console.WriteLine();
        }

        public static Assembly MyAssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
/*            Console.WriteLine("ASSEMBLY REQUESTED: " + args.RequestingAssembly.FullName);
            Console.WriteLine();*/

            try
            {
                string[] parts = args.Name.Split(',');

               // Assembly a = Assembly.ReflectionOnlyLoad(args.Name);
                if (parts[0].EndsWith(".resources"))
                {
                    return null;
                }

                Assembly assembly = System.Reflection.Assembly.Load(args.Name);
                if (assembly != null)
                    return assembly;
            }
            catch (Exception e)
            {
               
            }

            string[] Parts = args.Name.Split(',');
            string File = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Parts[0].Trim() + ".dll";

            return System.Reflection.Assembly.LoadFrom(File);
        }
    }
}
