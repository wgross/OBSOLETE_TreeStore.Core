using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface IRenameItem : IProviderNodeCapability
    {
        object RenameItemParameters => new RuntimeDefinedParameterDictionary();

        void RenameItem<CTX>(CTX providerContext, string path, string newName);
    }
}