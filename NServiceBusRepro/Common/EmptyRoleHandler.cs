using NServiceBus;
using NServiceBus.Hosting.Roles;
using NServiceBus.Unicast.Config;

namespace Common
{
    public class EmptyRoleHandler : IConfigureRole<IEmptyRole>
    {
        public ConfigUnicastBus ConfigureRole(IConfigureThisEndpoint specifier)
        {
            var configObject = Configure.Instance;
            return configObject.UnicastBus();
        }
    }
}