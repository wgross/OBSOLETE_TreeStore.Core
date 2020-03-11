using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IClearItem : IProviderNodeCapability
    {
        object ClearItemDynamicParamters => new RuntimeDefinedParameterDictionary();

        void ClearItem<CTX>(CTX providerContext);
    }
}