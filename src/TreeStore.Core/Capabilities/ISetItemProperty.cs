using System.Collections.Generic;
using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface ISetItemProperty : IProviderNodeCapability
    {
        object SetItemPropertyParameters => new RuntimeDefinedParameterDictionary();

        void SetItemProperties<CTX>(CTX providerContext, IEnumerable<PSPropertyInfo> properties);
    }
}