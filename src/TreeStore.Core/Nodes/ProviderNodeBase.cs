using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using TreeStore.Core.Capabilities;

namespace TreeStore.Core.Nodes
{
    public abstract class ProviderNodeBase : IGetItem, IGetItemProperty
    {
        public abstract IEnumerable<ProviderNodeBase> Resolve<CTX>(CTX providerContext, string nodeName);

        /// <summary>
        /// The IsContainer property indicates if this node may hold aother nodes as subnodes.
        /// It doesn't indicate if the conatiner has currenty child nodes.
        /// </summary>
        public abstract bool IsContainer { get; }

        /// <summary>
        /// Any path node must provide a name shis represenst it under its parents name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// A path node may implement on e or more capabilities. Every Powershell provioder Cmdlet requires one
        /// to process this node.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual (bool has, T? capability) TryGetCapability<T>() where T : class, IProviderNodeCapability
        {
            if (this is T cability)
                return (true, cability);
            return (false, default);
        }

        #region IGetItem

        public abstract PSObject GetItem<CTX>(CTX providerContext);

        #endregion IGetItem

        #region IGetItemProperties

        public IEnumerable<PSPropertyInfo> GetItemProperties<CTX>(CTX providerContext, IEnumerable<string> propertyNames)
        {
            if (propertyNames.Any())
                return this.GetItem(providerContext).Properties.Where(p => propertyNames.Contains(p.Name, StringComparer.OrdinalIgnoreCase));

            return this.GetItem(providerContext).Properties;
        }

        #endregion IGetItemProperties
    }
}