using System;
using System.Collections.Generic;
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
            var children = this.GetChildItems(providerContext);
            foreach (var child in children)
            {
                if (null == nodeName || StringComparer.InvariantCultureIgnoreCase.Equals(nodeName, child.Name))
                {
                    yield return child;
                }
            }
        }

        #region IGetChildItem

        public abstract IEnumerable<ProviderNodeBase> GetChildItems<CTX>(CTX providerContext);

        #endregion IGetChildItem
    }
}