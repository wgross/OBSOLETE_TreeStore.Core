using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface ISetItem : IProviderNodeCapability
    {
        object SetItemParameters => new RuntimeDefinedParameterDictionary();

        void SetItem<CTX>(CTX providerContext, object value);
    }
}