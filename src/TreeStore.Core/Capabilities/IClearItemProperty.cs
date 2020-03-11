using System.Collections.Generic;
using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IClearItemProperty : IProviderNodeCapability
    {
        object ClearItemPropertyParameters => new RuntimeDefinedParameterDictionary();

        void ClearItemProperty<CTX>(CTX providerContext, IEnumerable<string> propertyToClear);
    }
}