using Smoothie.Widgets;

namespace Smoothie
{
    public interface ISmoothieInstance
    {
        bool IsInitialized { get; }
        void Init();
        void Terminate();
        void Open<TWidget>(IViewModel model) where TWidget : IWidget;
        void Close(IViewModel model);
    }
}