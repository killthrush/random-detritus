using System;
using System.Reflection;

namespace Common
{
    public class ExecutionSandbox<T> : IDisposable where T : MarshalByRefObject
    {
        private AppDomain _domain;
        private Assembly[] _l;

        public ExecutionSandbox(string probingPath)
        {
            Type sandboxedType = typeof(T);
            AppDomainSetup domainInfo = new AppDomainSetup();
            domainInfo.ApplicationBase = probingPath;
            domainInfo.LoaderOptimization = LoaderOptimization.SingleDomain;
            //domaininfo.DisallowApplicationBaseProbing = true;

            _domain = AppDomain.CreateDomain(string.Format("Sandbox.{0}", sandboxedType.Namespace), null, domainInfo);
            _l = _domain.GetAssemblies();
            string assemblyName = sandboxedType.Assembly.FullName;
            Value = (T)_domain.CreateInstanceAndUnwrap(assemblyName, sandboxedType.FullName);
            _l = _domain.GetAssemblies();
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
