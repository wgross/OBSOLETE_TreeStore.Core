using System.Management.Automation;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface ICopyItem : IProviderNodeCapability
    {
        object CopyItemParameters => new RuntimeDefinedParameterDictionary();

        void CopyItem<CTX>(CTX providerContext, string sourceItemName, string destinationItemName, ProviderNodeBase? destinationNode);
    }
}