using System;
using System.IO;

namespace Common
{
    /// <summary>
    /// General-purpose class that can put a remoting proxy around a given type and create a new appdomain for it to run in.
    /// This effectively "sandboxes" the code being run and isolates its dependencies from other pieces of code.
    /// </summary>
    /// <typeparam name="T">The type of object that will be run in the sandbox.  Must be compatible with Remoting.</typeparam>
    public class ExecutionSandbox<T> : IDisposable 
        where T : MarshalByRefObject
    {
        /// <summary>
        /// Local copy of the sandbox app domain
        /// </summary>
        private AppDomain _domain;

        /// <summary>
        /// Reference of the proxy wrapper for T
        /// </summary>
        public T ObjectProxy { get; private set; }

        /// <summary>
        /// Creates an instance of ExecutionSandbox
        /// </summary>
        /// <param name="assemblyPath">The path where the assembly that contains type T may be found</param>
        public ExecutionSandbox(string assemblyPath)
        {
            Type sandboxedType = typeof (T);
            AppDomainSetup domainInfo = new AppDomainSetup();
            domainInfo.ApplicationBase = assemblyPath;
            _domain = AppDomain.CreateDomain(string.Format("Sandbox.{0}", sandboxedType.Namespace), null, domainInfo);

            string assemblyFileName = Path.Combine(assemblyPath, sandboxedType.Assembly.GetName().Name) + ".dll";
            object instanceAndUnwrap = _domain.CreateInstanceFromAndUnwrap(assemblyFileName, sandboxedType.FullName);
            ObjectProxy = (T)instanceAndUnwrap;
        }

        /// <summary>
        /// Allows safe cleanup of the sandbox app domain.
        /// </summary>
        public void Dispose()
        {
            if (_domain != null)
            {
                AppDomain.Unload(_domain);
                _domain = null;
            }

            ObjectProxy = null;
        }
    }
}
