using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IGetItem : IProviderNodeCapability
    {
        object GetItemParameters => new RuntimeDefinedParameterDictionary();

        PSObject GetItem<CTX>(CTX providerContext);
    }
}