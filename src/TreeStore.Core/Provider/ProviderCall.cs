using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Provider
{
    // Define other methods, classes and namespaces here
    public class ProviderMethodContext : IDisposable
    {
        public ProviderNodeBase? StartNode { get; }

        public ProviderBase Provider { get; }

        private readonly string callerMemberName;

        public IEnumerable<string> PathSegments()
        {
            var path = this.Path.ToLowerInvariant();
            path = new Regex(@"^[-_a-z0-9:]+:/?").Replace(path, "");

            return path.Split(new char[]
            {
                System.IO.Path.DirectorySeparatorChar,
                System.IO.Path.AltDirectorySeparatorChar,
            }, StringSplitOptions.RemoveEmptyEntries);
        }

        private IReadOnlyCollection<ProviderNodeBase>? pathNodes = null;

        public ProviderMethodContext(ProviderBase providerBase, string path, string callerMemberName)
        {
            this.callerMemberName = callerMemberName;
            this.Provider = providerBase;
            this.Path = path;
            this.StartNode = null;
        }

        #region Provide Path segmenst and nodes

        public string Path { get; }

        //private IEnumerable<string> PathSegments => this.pathSegments ??= this.SplitPath();

        //private readonly IEnumerable<string> pathSegments = null;

        
        #endregion Provide Path segmenst and nodes

        #region IDisposable

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Provider.WriteDebug($"StepOut:{this.callerMemberName} at {this.Path}");
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable

        #region Build provder node from path segments

        public IEnumerable<ProviderNodeBase> PathNodes => this.pathNodes ??= this.TraversePath().ToArray();

        private IEnumerable<ProviderNodeBase> TraversePath()
        {
            var innerNode = this.StartNode;
            IEnumerable<ProviderNodeBase> leafNodes = new[] { innerNode };

            foreach (var segment in this.PathSegments())
            {
                leafNodes = innerNode.Resolve(this, segment);
                if (leafNodes.Any())
                    innerNode = leafNodes.First(); // only one npde can be travered as an onner node.
                else return leafNodes;
            }

            return leafNodes;
        }

        #endregion Build provder node from path segments

        public void ForEachPathNode<T>(Action<ProviderBase, T> apply) where T : class
        {
            this.Provider.WriteDebug($"{this.callerMemberName}:Calling {typeof(T)} foreach node at {this.Path}");
            // interact with provider capability
            apply(this.Provider, default);
        }

        public object ForFirstPathNode<T>(Func<ProviderBase, T, object> apply) where T : class
        {
            this.Provider.WriteDebug($"{this.callerMemberName}:Calling {typeof(T)} for first node at {this.Path}");
            // interact with provider capability
            return apply(this.Provider, default);
        }

        public void ForFirstPathNode(Func<ProviderBase, ProviderNodeBase, object> apply)
        {
            // interact with provider capability
            apply(this.Provider, default);
        }

        public R ForFirstPathNode<T, R>(Func<ProviderBase, T, R> apply) where T : class
        {
            this.Provider.WriteDebug($"{this.callerMemberName}:Calling {typeof(T)} for first node at {this.Path}");
            // interact with provider capability
            return apply(this.Provider, default);
        }
    }
}