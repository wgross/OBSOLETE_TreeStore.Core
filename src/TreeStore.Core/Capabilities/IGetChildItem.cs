using System.Collections.Generic;
using System.Management.Automation;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface IGetChildItem : IProviderNodeCapability
    {
        public object GetChildItemParameters => new RuntimeDefinedParameterDictionary();

        IEnumerable<ProviderNodeBase> GetChildItems<CTX>(CTX providerContext);
    }
}