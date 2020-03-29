using System.Management.Automation.Provider;
using System.Runtime.CompilerServices;

namespace TreeStore.Core.Provider
{
    public abstract partial class ProviderBase : ContainerCmdletProvider
    {
        protected virtual ProviderMethodContext CreateContext(string path, [CallerMemberName]string callerMemberName = "")
        {
            this.WriteDebug($"StepIn:{callerMemberName} at {path}");
            return new ProviderMethodContext(this, path, callerMemberName);
        }
    }
}