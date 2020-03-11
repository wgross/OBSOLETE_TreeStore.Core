using System.Management.Automation;
using System.Management.Automation.Provider;

namespace TreeStore.Core.Capabilities
{
    public interface ISetItemContent : IProviderNodeCapability
    {
        IContentWriter GetContentWriter<CTX>(CTX providerContext);

        object GetContentWriterDynamicParameters => new RuntimeDefinedParameterDictionary();
    }
}