using Smoothie.Widgets;

namespace Smoothie.Layers
{
    public interface IUiLayer
    {
        void Open<TWidget>(IViewModel model, TWidget widget, BaseView view);
        void Close();
        bool CanAcceptWidget<TWidget>() where TWidget : IWidget;
    }
}