using System;
using System.Collections.Generic;
using System.Reflection;

namespace CmdletThree
{


    public static class AssemblyLocator
    {

        static Dictionary<string, Assembly> assemblies;
        private static bool init;


        public static void Init()
        {
            if (!init)
            {
                assemblies = new Dictionary<string, Assembly>();
                AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                init = true;
            }
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {

            Assembly assembly = null;

            assemblies.TryGetValue(args.Name, out assembly);

            return assembly;

        }



        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {

            Assembly assembly = args.LoadedAssembly;

            assemblies[assembly.FullName] = assembly;

        }

    } 
}
