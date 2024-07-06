namespace Smoothie.Widgets
{
    public abstract class UiScreen<TModel, TView> : WidgetBase<TModel, TView>
        where TModel : IViewModel where TView : BaseView
    {
    }
}