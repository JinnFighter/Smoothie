using Smoothie.Widgets;

namespace Smoothie.Pooling
{
    public interface IPoolProvider
    {
        void Init(SmoothieConfig smoothieConfig);
        void Terminate();
        T Get<T>() where T : BaseView;
        void Release<T>(T obj) where T : BaseView;
    }
}
