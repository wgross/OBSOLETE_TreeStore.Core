using TreeStore.Core.Capabilities;

namespace TreeStore.Core.Provider
{
    public abstract partial class ProviderBase
    {
        #region GetItem

        protected override void GetItem(string path)
        {
            using var ctx = CreateContext(path);

            var node = ctx.GetPathNode<IGetItem>();
            if (node is null)
                ctx.Provider.WriteItemObject(null, ctx.Path, isContainer: false);
            else
                ctx.Provider.WriteItemObject(node.GetItem(ctx), ctx.Path, node.IsContainer);
        }

        protected override object GetItemDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<IGetItem>()?.GetItemParameters(ctx) ?? this.EmptyRuntimeDefinedParameters;
        }

        #endregion GetItem

        #region ClearItem

        protected override void ClearItem(string path)
        {
            using var ctx = CreateContext(path);

            ctx.GetPathNode<IClearItem>()?.ClearItem(ctx);
        }

        protected override object ClearItemDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<IClearItem>()?.ClearItemDynamicParamters(ctx) ?? this.EmptyRuntimeDefinedParameters;
        }

        #endregion ClearItem

        #region InvokeDefaultAction

        protected override void InvokeDefaultAction(string path)
        {
            using var ctx = CreateContext(path);

            ctx.GetPathNode<IInvokeItem>()?.InvokeItem(ctx);
        }

        protected override object InvokeDefaultActionDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<IInvokeItem>()?.InvokeItemParameters(ctx) ?? this.EmptyRuntimeDefinedParameters;
        }

        #endregion InvokeDefaultAction

        #region ItemExists

        protected override bool ItemExists(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<IItemExists>()?.ItemExists(ctx) ?? false;
        }

        protected override object ItemExistsDynamicParameters(string path)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<IItemExists>()?.ItemExistsParameters(ctx) ?? this.EmptyRuntimeDefinedParameters;
        }

        #endregion ItemExists

        #region SetItem

        protected override void SetItem(string path, object value)
        {
            using var ctx = CreateContext(path);

            ctx.GetPathNode<ISetItem>()?.SetItem(ctx, value);
        }

        protected override object SetItemDynamicParameters(string path, object value)
        {
            using var ctx = CreateContext(path);

            return ctx.GetPathNode<ISetItem>()?.SetItemParameters(ctx) ?? this.EmptyRuntimeDefinedParameters;
        }

        #endregion SetItem
    }
}