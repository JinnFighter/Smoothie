using Smoothie.Widgets;
using UnityEngine;

namespace Smoothie.Pooling
{
    public abstract class BasePoolProvider : MonoBehaviour, IPoolProvider
    {
        public abstract void Init(SmoothieConfig config);
        public abstract void Terminate();
        public abstract T Get<T>() where T : BaseView;
        public abstract void Release<T>(T obj) where T : BaseView;
    }
}