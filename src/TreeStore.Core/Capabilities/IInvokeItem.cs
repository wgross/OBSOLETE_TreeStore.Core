using System.Collections.Generic;
using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IInvokeItem : IProviderNodeCapability
    {
        object InvokeItemParameters => new RuntimeDefinedParameterDictionary();

        IEnumerable<object> InvokeItem<CTX>(CTX providerContext);
    }
}