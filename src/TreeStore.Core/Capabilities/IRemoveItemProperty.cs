using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IRemoveItemProperty : IProviderNodeCapability
    {
        object RemoveItemPropertyParameters => new RuntimeDefinedParameterDictionary();

        void RemoveItemProperty<CTX>(CTX providerContext, string propertyName);
    }
}