using System;
using Smoothie.Scripts.Widgets;
using UnityEngine;

namespace Smoothie.Scripts.Pooling
{
    public abstract class BasePoolProvider : MonoBehaviour, IPoolProvider
    {
        public abstract void Init(SmoothieConfig config);
        public abstract void Terminate();
        public abstract T Get<T>() where T : BaseView;
        public abstract BaseView Get(Type type);
        public abstract void Release<T>(T obj) where T : BaseView;
    }
}