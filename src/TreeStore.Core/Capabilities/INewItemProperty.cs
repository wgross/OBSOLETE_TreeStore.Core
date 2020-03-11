using System.Management.Automation;

namespace TreeStore.Core.Capabilities
{
    public interface INewItemProperty : IProviderNodeCapability
    {
        object NewItemPropertyParameters => new RuntimeDefinedParameterDictionary();

        void NewItemProperty<CTX>(CTX providerContext, string propertyName, string propertyTypeName, object newItemValue);
    }
}