using System.Collections.Generic;
using System.Linq;

namespace TreeStore.Core.Nodes
{
    /// <summary>
    /// A leaf node is never a container and has never child nodes in the the file system.
    /// </summary>
    public abstract class LeafNodeBase : ProviderNodeBase
    {
        public override IEnumerable<ProviderNodeBase> Resolve<CTX>(CTX providerContext, string nodeName) => Enumerable.Empty<ProviderNodeBase>();

        public override bool IsContainer => false;
    }
}