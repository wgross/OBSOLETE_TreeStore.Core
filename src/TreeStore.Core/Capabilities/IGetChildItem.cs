using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface IGetChildItem : IProviderNodeCapability
    {
        public object GetChildItemParameters => new RuntimeDefinedParameterDictionary();

        IEnumerable<ProviderNodeBase> GetChildItems<CTX>(CTX providerContext);

        bool HasChildItems<CTX>(CTX ctx) => this.GetChildItems(ctx).Any();
    }
}