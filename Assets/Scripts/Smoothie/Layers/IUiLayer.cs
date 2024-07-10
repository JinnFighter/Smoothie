using Smoothie.Widgets;

namespace Smoothie.Layers
{
    public interface IUiLayer
    {
        void Open();
        void Close();
        bool CanAcceptWidget<TWidget>() where TWidget : IWidget;
    }
}