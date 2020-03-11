using System.Collections.Generic;
using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IGetItemProperty : IProviderNodeCapability
    {
        object GetItemPropertyParameters => new RuntimeDefinedParameterDictionary();

        IEnumerable<PSPropertyInfo> GetItemProperties<CTX>(CTX providerContext, IEnumerable<string> propertyNames);
    }
}