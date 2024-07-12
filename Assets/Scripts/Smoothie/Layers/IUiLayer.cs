using Smoothie.Widgets;

namespace Smoothie.Layers
{
    public interface IUiLayer
    {
        WidgetReference Open<TWidget>(IViewModel model, TWidget widget, BaseView view) where TWidget : IWidget;
        WidgetReference Close(IViewModel model);
        bool CanAcceptWidget(IViewModel model);
        bool ContainsWidget(IViewModel model);
    }
}