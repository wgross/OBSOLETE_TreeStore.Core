namespace TreeStore.Core.Capabilities
{
    public interface IProviderNodeCapability
    {
        bool IsContainer { get; }

        string Name { get; }

        public (bool has, T? capability) TryGetCapability<T>() where T : class, IProviderNodeCapability;
    }
}