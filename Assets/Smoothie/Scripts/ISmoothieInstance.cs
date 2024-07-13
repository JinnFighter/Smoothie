using Smoothie.Scripts.Widgets;

namespace Smoothie.Scripts
{
    public interface ISmoothieInstance
    {
        bool IsInitialized { get; }
        void Init();
        void Terminate();
        void Open<TWidget>(IViewModel model) where TWidget : IWidget;
        void Close<TWidget>(IViewModel model) where TWidget : IWidget;
    }
}