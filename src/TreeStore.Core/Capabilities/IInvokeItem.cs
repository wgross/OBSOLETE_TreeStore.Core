using System.Collections.Generic;

namespace TreeStore.Core.Capabilities
{
    public interface IInvokeItem : IProviderNodeCapability
    {
        object InvokeItemParameters<CTX>(CTX providerContext);

        IEnumerable<object> InvokeItem<CTX>(CTX providerContext);
    }
}