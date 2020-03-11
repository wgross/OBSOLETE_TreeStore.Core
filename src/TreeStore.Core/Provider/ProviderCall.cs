using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Provider;
using System.Text.RegularExpressions;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Provider
{
    // Define other methods, classes and namespaces here
    public class ProviderMethodContext
    {
        public ProviderNodeBase StartNode { get; internal set; }

        public string Path { get; internal set; }

        public CmdletProvider Provider { get; internal set; }

        public IEnumerable<string> PathSegments()
        {
            var path = this.Path.ToLowerInvariant().Replace('\\', '/');
            path = new Regex(@"^[-_a-z0-9:]+:/?").Replace(path, "");

            return path.Split(new char[]
            {
                System.IO.Path.DirectorySeparatorChar,
                System.IO.Path.AltDirectorySeparatorChar,
            }, StringSplitOptions.RemoveEmptyEntries);

            IEnumerable<ProviderNodeBase> factories = new[] { this.StartNode };
        }
    }

    public readonly struct ProviderMethodCall<CTX> : IDisposable
        where CTX : ProviderMethodContext
    {
        private readonly string memberName;
        private readonly CTX context;

        internal ProviderMethodCall(CTX context, string memberName)
        {
            this.memberName = memberName;
            this.context = context;
        }

        public void ForEachPathNode<T>(Action<CTX, T> apply) where T : class
        {
            // interact with provider capability
            apply(this.context, default);
        }

        public object ForFirstPathNode<T>(Func<CTX, T, object> apply) where T : class
        {
            // interact with provider capability
            return apply(this.context, default);
        }

        public R ForFirstPathNode<T,R>(Func<CTX, T, R> apply) where T : class
        {
            // interact with provider capability
            return apply(this.context, default);
        }

        public void Dispose() => this.context.Provider.WriteDebug($"StepOut:{this.memberName}");

        private IEnumerable<ProviderNodeBase> TraversePath()
        {
            var nextNode = this.context.StartNode;
            IEnumerable<ProviderNodeBase> resultNodes = new[] { nextNode };

            foreach (var segment in this.context.PathSegments())
            {
                resultNodes = nextNode.Resolve(this.context, segment);
                if (resultNodes.Any())
                    nextNode = resultNodes.First();
                else return resultNodes;
            }

            return resultNodes;
        }
    }
}