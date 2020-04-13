using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Runtime.CompilerServices;

namespace TreeStore.Core.Provider
{
    public abstract partial class ProviderBase : NavigationCmdletProvider
    {
        private object EmptyRuntimeDefinedParameters => new RuntimeDefinedParameterDictionary();

        protected virtual ProviderCallContext CreateContext(string path, [CallerMemberName]string callerMemberName = "")
        {
            this.WriteDebug($"StepIn:{callerMemberName} at {path}");
            return new ProviderCallContext(this, path, callerMemberName);
        }
    }
}