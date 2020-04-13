using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IItemExists : IProviderNodeCapability
    {
        object ItemExistsParameters<CTX>(CTX providerContext);

        bool ItemExists<CTX>(CTX providerContext);
    }
}