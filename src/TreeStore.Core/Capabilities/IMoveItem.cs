using System.Management.Automation;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface IMoveItem : IProviderNodeCapability
    {
        object MoveItemParameters => new RuntimeDefinedParameterDictionary();

        void MoveItem<CTX>(CTX providerContext, string path, string movePath, ProviderNodeBase destinationContainer);
    }
}