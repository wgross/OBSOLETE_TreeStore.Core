using System.Management.Automation;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface IMoveItemPropertySource : IRemoveItemProperty
    {
        object MoveItemPropertyParameters => new RuntimeDefinedParameterDictionary();
    }

    public interface IMoveItemPropertyDestination : IProviderNodeCapability
    {
        void MoveItemProperty<CTX>(CTX providerContext, string sourceProperty, string destinationProperty, ProviderNodeBase sourceItemProvider);
    }
}