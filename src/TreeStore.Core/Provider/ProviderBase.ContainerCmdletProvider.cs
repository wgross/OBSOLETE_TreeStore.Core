using System.Management.Automation;
using TreeStore.Core.Capabilities;

namespace TreeStore.Core.Provider
{
    public partial class ProviderBase
    {
        #region GetChildItem

        protected override bool HasChildItems(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<IGetChildItem>()?.HasChildItems(ctx) ?? false;
        }

        protected override object GetChildItemsDynamicParameters(string path, bool recurse)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<IGetChildItem>()?.GetChildItemParameters(ctx, recurse) ?? this.EmptyRuntimeDefinedParameters;
        }

        override protected void GetChildItems(string path, bool recurse)
        {
            using var ctx = CreateContext(path);

            var parentNode = ctx.GetPathNode<IGetChildItem>();
            if (parentNode is null)
                return;

            foreach (var childNode in parentNode.GetChildItems(ctx, recurse))
            {
                var (implements, getItem) = childNode.TryGetCapability<IGetItem>();
                if (implements)
                    this.WriteItemObject(childNode.GetItem(ctx), this.MakePath(ctx.Path, childNode.Name), childNode.IsContainer);
            }
        }

        override protected void GetChildItems(string path, bool recurse, uint depth)
        {
            using var ctx = CreateContext(path);

            var parentNode = ctx.GetPathNode<IGetChildItem>();
            if (parentNode is null)
                return;

            foreach (var childNode in parentNode.GetChildItems(ctx, recurse, depth))
            {
                var (implements, getItem) = childNode.TryGetCapability<IGetItem>();
                if (implements)
                    this.WriteItemObject(childNode.GetItem(ctx), this.MakePath(ctx.Path, childNode.Name), childNode.IsContainer);
            }
        }

        #endregion GetChildItem

        #region GetChildNames

        protected override object GetChildNamesDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<IGetChildItem>()?.GetChildItemParameters(ctx, false) ?? this.EmptyRuntimeDefinedParameters;
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            using var ctx = CreateContext(path);

            var parentNode = ctx.GetPathNode<IGetChildItem>();
            if (parentNode is null)
                return;

            foreach (var childNode in parentNode.GetChildItems(ctx, false))
            {
                var (implements, getItem) = childNode.TryGetCapability<IGetItem>();
                if (implements)
                    this.WriteItemObject(childNode.GetItem(ctx), this.MakePath(ctx.Path, childNode.Name), childNode.IsContainer);
            }
        }

        #endregion GetChildNames

        #region CopyItem

        protected override object CopyItemDynamicParameters(string path, string copyPath, bool recurse)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<ICopyItem>()?.CopyItemParameters(ctx, copyPath, recurse) ?? this.EmptyRuntimeDefinedParameters;
        }

        protected override void CopyItem(string path, string copyPath, bool recurse)
        {
            using var ctx = CreateContext(path);

            var nodeToCopy = ctx.GetPathNode<ICopyItem>();
            if (nodeToCopy is null)
                return;

            var destination = ctx.GetPathNodeOrParent(copyPath);
            if (destination.node is null && destination.parentNode is null)
                return;

            if (destination.node is null)
                nodeToCopy.CopyItem(ctx, destination.node);
            else
                nodeToCopy.CopyItem(ctx, copyPath, destination.parentNode);
        }

        #endregion CopyItem

        protected override bool ConvertPath(string path, string filter, ref string updatedPath, ref string updatedFilter)
        {
            return base.ConvertPath(path, filter, ref updatedPath, ref updatedFilter);
        }
    }
}