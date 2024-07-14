namespace Smoothie.Scripts.Widgets
{
    public abstract class UiDialog<TModel, TView> : WidgetBase<TModel, TView>
        where TModel : IViewModel where TView : BaseView
    {
    }
}