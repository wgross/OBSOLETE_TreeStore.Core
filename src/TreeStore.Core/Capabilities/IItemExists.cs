using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IItemExists : IProviderNodeCapability
    {
        object ItemExistsParameters => new RuntimeDefinedParameterDictionary();

        bool ItemExists<CTX>(CTX providerContext);
    }
}