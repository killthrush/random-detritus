using System;
using System.IO;

namespace Common
{
    public class ExecutionSandbox<T> : IDisposable 
        where T : MarshalByRefObject
    {
        private AppDomain _domain;

        public ExecutionSandbox(string probingPath)
        {
            Type sandboxedType = typeof (T);
            AppDomainSetup domainInfo = new AppDomainSetup();
            domainInfo.ApplicationBase = probingPath;
            _domain = AppDomain.CreateDomain(string.Format("Sandbox.{0}", sandboxedType.Namespace), null, domainInfo);

            string assemblyFileName = Path.Combine(probingPath, sandboxedType.Assembly.GetName().Name) + ".dll";
            object instanceAndUnwrap = _domain.CreateInstanceFromAndUnwrap(assemblyFileName, sandboxedType.FullName);
            Value = (T)instanceAndUnwrap;
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
}
