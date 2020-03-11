using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IClearItemContent : IProviderNodeCapability
    {
        void ClearContent<CTX>(CTX providerContext);

        object ClearContentDynamicParameters<CTX>(CTX providerContext) => new RuntimeDefinedParameterDictionary();
    }
}