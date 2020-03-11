using System.Management.Automation;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface ICopyItemPropertySource : IProviderNodeCapability
    {
        object CopyItemPropertyParameters => new RuntimeDefinedParameterDictionary();
    }

    public interface ICopyItemPropertyDestination : IProviderNodeCapability
    {
        void CopyItemProperty<CTX>(CTX providerContext, string sourcePropertyName, string destinationPropertyName, ProviderNodeBase sourceNode);
    }
}