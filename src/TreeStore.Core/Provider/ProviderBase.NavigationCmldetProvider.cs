namespace TreeStore.Core.Provider
{
    public abstract partial class ProviderBase
    {
        protected override string MakePath(string parent, string child)
        {
            return base.MakePath(parent, child);
        }
    }
}