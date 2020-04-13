using System.Collections.Generic;
using System.Linq;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface IGetChildItem : IProviderNodeCapability
    {
        public object GetChildItemParameters<CTX>(CTX providerContext, bool recurse);

        IEnumerable<ProviderNodeBase> GetChildItems<CTX>(CTX providerContext, bool recurse);

        IEnumerable<ProviderNodeBase> GetChildItems<CTX>(CTX providerContext, bool recurse, uint depth);

        bool HasChildItems<CTX>(CTX ctx) => this.GetChildItems(ctx, recurse: false).Any();
    }
}