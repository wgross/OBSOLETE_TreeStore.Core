using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IRenameItemProperty : IProviderNodeCapability
    {
        object RenameItemPropertyParameters => new RuntimeDefinedParameterDictionary();

        void RenameItemProperty<CTX>(CTX providerContext, string sourcePropertyNamee, string destinationPropertyName);
    }
}