using TreeStore.Core.Nodes;

namespace TreeStore.Core.Capabilities
{
    public interface ICopyItem : IProviderNodeCapability
    {
        object CopyItemParameters<CTX>(CTX providerContext, string copyPath, bool recurse);

        /// <summary>
        /// Copy this node to the new noew as a new child node.
        /// The name of the node matches the nodes current name
        /// </summary>
        /// <typeparam name="CTX"></typeparam>
        /// <param name="providerContext"></param>
        /// <param name="destinationNode"></param>
        void CopyItem<CTX>(CTX providerContext, ProviderNodeBase? destinationNode);

        /// <summary>
        /// Copy this node as a node with a new name to teh destination node.
        /// </summary>
        /// <typeparam name="CTX"></typeparam>
        /// <param name="providerContext"></param>
        /// <param name="destinationItemName"></param>
        /// <param name="destinationNode"></param>
        void CopyItem<CTX>(CTX providerContext, string destinationItemName, ProviderNodeBase? destinationNode);
    }
}