using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface ISetItem : IProviderNodeCapability
    {
        object SetItemParameters<CTX>(CTX providerContext);

        void SetItem<CTX>(CTX providerContext, object value);
    }
}