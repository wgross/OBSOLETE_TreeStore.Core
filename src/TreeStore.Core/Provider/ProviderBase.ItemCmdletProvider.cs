using TreeStore.Core.Capabilities;

namespace TreeStore.Core.Provider
{
    public abstract partial class ProviderBase
    {
        #region GetItem

        protected override void GetItem(string path)
        {
            using var call = StepIn(CreateContext(path));

            call.ForEachPathNode<IGetItem>((ctx, gi) =>
            {
                ctx.Provider.WriteItemObject(gi.GetItem(ctx), ctx.Path, gi.IsContainer);
            });
        }

        protected override object GetItemDynamicParameters(string path)
        {
            using var call = StepIn(CreateContext(path));

            return call.ForFirstPathNode<IGetItem>((ctx, gi) => gi.GetItemParameters);
        }

        #endregion GetItem

        #region ClearItem

        protected override void ClearItem(string path)
        {
            using var call = StepIn(CreateContext(path));

            call.ForEachPathNode<IClearItem>((ctx, ci) => ci.ClearItem(ctx));
        }

        protected override object ClearItemDynamicParameters(string path)
        {
            using var call = StepIn(CreateContext(path));

            return call.ForFirstPathNode<IClearItem>((ctx, ci) => ci.ClearItemDynamicParamters);
        }

        #endregion ClearItem

        #region InvokeDefaultAction

        protected override void InvokeDefaultAction(string path)
        {
            using var call = StepIn(CreateContext(path));

            call.ForEachPathNode<IInvokeItem>((ctx, ii) => ii.InvokeItem(ctx));
        }

        protected override object InvokeDefaultActionDynamicParameters(string path)
        {
            using var call = StepIn(CreateContext(path));

            return call.ForFirstPathNode<IInvokeItem>((ctx, ci) => ci.InvokeItemParameters);
        }

        #endregion InvokeDefaultAction

        #region ItemExists

        protected override bool ItemExists(string path)
        {
            using var call = StepIn(CreateContext(path));

            return call.ForFirstPathNode<IItemExists, bool>((ctx, ie) => ie.ItemExists(ctx));
        }

        protected override object ItemExistsDynamicParameters(string path)
        {
            using var call = StepIn(CreateContext(path));

            return call.ForFirstPathNode<IItemExists>((ctx, ie) => ie.ItemExistsParameters);
        }

        #endregion ItemExists

        #region SetItem

        protected override void SetItem(string path, object value)
        {
            using var call = StepIn(CreateContext(path));

            call.ForEachPathNode<ISetItem>((ctx, si) => si.SetItem(ctx, value));
        }

        protected override object SetItemDynamicParameters(string path, object value)
        {
            using var call = StepIn(CreateContext(path));

            return call.ForFirstPathNode<ISetItem>((ctx, si) => si.SetItemParameters);
        }

        #endregion SetItem
    }
}