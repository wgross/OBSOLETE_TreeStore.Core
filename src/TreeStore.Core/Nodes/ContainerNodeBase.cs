using System;
using System.Collections.Generic;
using System.Linq;
using TreeStore.Core.Capabilities;

namespace TreeStore.Core.Nodes
{
    /// <summary>
    /// A container node may have child nodes.
    /// </summary>
    public abstract class ContainerNodeBase : ProviderNodeBase, IGetChildItem
    {
        public override bool IsContainer => true;

        public override IEnumerable<ProviderNodeBase> Resolve<CTX>(CTX providerContext, string nodeName)
        {
            var children = this.GetChildItems(providerContext, false);
            foreach (var child in children)
            {
                if (null == nodeName || StringComparer.InvariantCultureIgnoreCase.Equals(nodeName, child.Name))
                {
                    yield return child;
                }
            }
        }

        #region IGetChildItem

        public virtual IEnumerable<ProviderNodeBase> GetChildItems<CTX>(CTX providerContext, bool recurse)
            => this.GetChildItems(providerContext, recurse, uint.MaxValue);

        public abstract IEnumerable<ProviderNodeBase> GetChildItems<CTX>(CTX providerContext, bool recurse, uint depth);

        public abstract object GetChildItemParameters<CTX>(CTX providerContext, bool recurse);

        public virtual bool HasChildItems<CTX>(CTX providerContext) => this.GetChildItems(providerContext, recurse: false).Any();

        #endregion IGetChildItem
    }
}