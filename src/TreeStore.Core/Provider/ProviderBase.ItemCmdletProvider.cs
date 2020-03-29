using TreeStore.Core.Capabilities;

namespace TreeStore.Core.Provider
{
    public abstract partial class ProviderBase
    {
        #region GetItem

        protected override void GetItem(string path)
        {
            using var ctx = CreateContext(path);

            ctx.ForEachPathNode<IGetItem>((ctx, gi) =>
            {
                ctx.WriteItemObject(gi.GetItem(ctx), path, gi.IsContainer);
            });
        }

        protected override object GetItemDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.ForFirstPathNode<IGetItem>((ctx, gi) => gi.GetItemParameters);
        }

        #endregion GetItem

        #region ClearItem

        protected override void ClearItem(string path)
        {
            using var ctx = CreateContext(path);

            ctx.ForEachPathNode<IClearItem>((ctx, ci) => ci.ClearItem(ctx));
        }

        protected override object ClearItemDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.ForFirstPathNode<IClearItem>((ctx, ci) => ci.ClearItemDynamicParamters);
        }

        #endregion ClearItem

        #region InvokeDefaultAction

        protected override void InvokeDefaultAction(string path)
        {
            using var ctx = CreateContext(path);

            ctx.ForEachPathNode<IInvokeItem>((ctx, ii) => ii.InvokeItem(ctx));
        }

        protected override object InvokeDefaultActionDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.ForFirstPathNode<IInvokeItem>((ctx, ci) => ci.InvokeItemParameters);
        }

        #endregion InvokeDefaultAction

        #region ItemExists

        protected override bool ItemExists(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.ForFirstPathNode<IItemExists, bool>((ctx, ie) => ie.ItemExists(ctx));
        }

        protected override object ItemExistsDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.ForFirstPathNode<IItemExists>((ctx, ie) => ie.ItemExistsParameters);
        }

        #endregion ItemExists

        #region SetItem

        protected override void SetItem(string path, object value)
        {
            using var ctx = CreateContext(path);

            ctx.ForEachPathNode<ISetItem>((ctx, si) => si.SetItem(ctx, value));
        }

        protected override object SetItemDynamicParameters(string path, object value)
        {
            using var ctx = CreateContext(path);

            return ctx.ForFirstPathNode<ISetItem>((ctx, si) => si.SetItemParameters);
        }

        #endregion SetItem
    }
}