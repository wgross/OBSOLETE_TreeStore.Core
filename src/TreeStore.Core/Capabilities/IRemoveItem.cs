using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IRemoveItem : IProviderNodeCapability
    {
        object RemoveItemParameters => new RuntimeDefinedParameterDictionary();

        void RemoveItem<CTX>(CTX providerContext, string path);
    }
}