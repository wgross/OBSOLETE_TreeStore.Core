using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IGetItem : IProviderNodeCapability
    {
        object GetItemParameters<CTX>(CTX providerContext);

        PSObject GetItem<CTX>(CTX providerContext);
    }
}