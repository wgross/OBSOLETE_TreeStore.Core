using System.Collections.Generic;
using System.Management.Automation;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface INewItem : IProviderNodeCapability
    {
        IEnumerable<string> NewItemTypeNames { get; }

        object NewItemParameters => new RuntimeDefinedParameterDictionary();

        ProviderNodeBase NewItem<CTX>(CTX providerContext, string newItemChildPath, string itemTypeName, object newItemValue);
    }
}