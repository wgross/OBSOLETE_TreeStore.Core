using System.Linq;
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

            return ctx.ForFirstPathNode<IGetChildItem, bool>((ctx, gi) =>
            {
                return gi.HasChildItems(ctx);
            });
        }

        protected override object GetChildItemsDynamicParameters(string path, bool recurse)
        {
            using var ctx = CreateContext(path);

            return ctx.ForFirstPathNode<IGetChildItem>((ctx, gi) => gi.GetChildItemParameters);
        }

        override protected void GetChildItems(string path, bool recurse)
        {
            using var call = CreateContext(path);

            call.ForEachPathNode<IGetChildItem>((ctx, gi) => gi.GetChildItems(ctx));
        }

        override protected void GetChildItems(string path, bool recurse, uint depth)
        {
            using var ctx = CreateContext(path);

            ctx.ForEachPathNode<IGetChildItem>((ctx, gi) =>
            {
                // recurse?
                // path muss child name enthalten
                gi.GetChildItems(ctx).ToList().ForEach(n => ctx.WriteItemObject(n, path, n.IsContainer));
            });
        }

        #endregion GetChildItem

        #region GetChildNames

        protected override object GetChildNamesDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.ForFirstPathNode<IGetChildItem>((ctx, gi) => gi.GetChildItemParameters);
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            using var ctx = CreateContext(path);

            ctx.ForEachPathNode<IGetChildItem>((ctx, gi) =>
            {
                // recurse?
                // path muss child name enthalten
                gi.GetChildItems(ctx).ToList().ForEach(n => ctx.WriteItemObject(n, path, n.IsContainer));
            });
        }

        #endregion GetChildNames

        #region CopyItem

        protected override object CopyItemDynamicParameters(string path, string copyPath, bool recurse)
        {
            using var call = CreateContext(path);

            return call.ForFirstPathNode<ICopyItem>((ctx, gi) => gi.CopyItemParameters);
        }

        protected override void CopyItem(string path, string copyPath, bool recurse)
        {
            using var ctx = CreateContext(path);

            // evaluate copy destination first
            ctx.ForFirstPathNode((ctx, ci) =>
            {
                return 1;
                // ci.CopyItem(ctx, ctx.Path, (string)ctx.DestinationPath, null);
            });

            ctx.ForEachPathNode<ICopyItem>((ctx, ci) =>
            {
                ci.CopyItem(ctx, path, copyPath, null);
            });
        }

        #endregion CopyItem

        protected override bool ConvertPath(string path, string filter, ref string updatedPath, ref string updatedFilter)

        {
            return base.ConvertPath(path, filter, ref updatedPath, ref updatedFilter);
        }
    }
}