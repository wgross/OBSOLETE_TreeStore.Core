using System.Management.Automation;
using System.Management.Automation.Provider;

namespace TreeStore.Core.Capabilities
{
    public interface IGetItemContent : IProviderNodeCapability
    {
        IContentReader GetContentReader<CTX>(CTX providerContext);

        object GetContentReaderDynamicParameters => new RuntimeDefinedParameterDictionary();
    }
}