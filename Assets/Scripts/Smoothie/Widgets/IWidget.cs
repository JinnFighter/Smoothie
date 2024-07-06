namespace Smoothie.Widgets
{
    public interface IWidget
    {
        void Init();
        void Terminate();
        void Setup(IViewModel model, BaseView view);
    }
}