namespace TreeStore.Core.Capabilities
{
    public interface IClearItem : IProviderNodeCapability
    {
        object ClearItemDynamicParamters<CTX>(CTX providerContext);

        void ClearItem<CTX>(CTX providerContext);
    }
}