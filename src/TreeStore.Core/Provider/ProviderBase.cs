using System.Management.Automation.Provider;
using System.Runtime.CompilerServices;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Provider
{
    public abstract partial class ProviderBase : ItemCmdletProvider
    {
        protected abstract ProviderNodeBase RootNode { get; }

        protected ProviderMethodContext CreateContext(string path) => new ProviderMethodContext
        {
            Path = path,
            Provider = this,
            StartNode = this.RootNode
        };

        protected ProviderMethodCall<ProviderMethodContext> StepIn(ProviderMethodContext contexr, [CallerMemberName]string methodName = "")
        {
            return new ProviderMethodCall<ProviderMethodContext>(new ProviderMethodContext(), methodName);
        }
    }
}