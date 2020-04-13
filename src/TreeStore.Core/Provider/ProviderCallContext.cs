using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text.RegularExpressions;
using TreeStore.Core.Nodes;

namespace TreeStore.Core.Provider
{
    // Define other methods, classes and namespaces here
    public class ProviderCallContext : IDisposable
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

        public ProviderCallContext(ProviderBase providerBase, string path, string callerMemberName)
        {
            this.callerMemberName = callerMemberName;
            this.Provider = providerBase;
            this.Path = path;
            this.StartNode = null;
        }

        #region Provide Path segments and nodes

        public string Path { get; }

        //private IEnumerable<string> PathSegments => this.pathSegments ??= this.SplitPath();

        //private IEnumerable<string> SplitPath()
        //{
        //    yield return string.Empty;

        //    //string? childName = null;
        //    //var path = this.Path;
        //    //do
        //    //{
        //    //    childName = this.Provider.SessionState.Path.ParseChildName(this.Path);
        //    //}
        //    //while (!string.IsNullOrEmpty(childName));
        //}

        //private IEnumerable<string>? pathSegments = null;

        #endregion Provide Path segments and nodes

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

        #region Retrueve the path node to call

        protected ProviderNodeBase? GetPathNode()
        {
            this.Provider.WriteDebug($"{this.callerMemberName}: Resolving node at {this.Path}");
            return default;
        }

        public T GetPathNode<T>() where T : class
        {
            var pathNode = this.GetPathNode();

            if (pathNode is null)
            {
                throw new ItemNotFoundException(this.Path);
            }
            else if (pathNode is T t)
            {
                this.Provider.WriteDebug($"{this.callerMemberName}: Node at {this.Path} supports capability {nameof(T)}");
                return t;
            }
            else
            {
                throw new PSNotSupportedException($"{this.callerMemberName}: Node at {this.Path} doesn't support capability {nameof(T)}");
            }
        }

        public (ContainerNodeBase? parentNode, ContainerNodeBase? node) GetPathNodeOrParent(string copyPath)
        {
            return (null, null);
        }

        public void ForEachPathNode<T>(Action<ProviderBase, T> apply) where T : class
        {
            this.Provider.WriteDebug($"{this.callerMemberName}:Calling {typeof(T)} foreach node at {this.Path}");
            // interact with provider capabilitym
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

        #endregion Retrueve the path node to call
    }
}