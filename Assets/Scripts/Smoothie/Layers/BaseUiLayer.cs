using Smoothie.Widgets;
using UnityEngine;

namespace Smoothie.Layers
{
    public abstract class BaseUiLayer : MonoBehaviour, IUiLayer
    {
        public abstract void Open<TWidget>(IViewModel model, TWidget widget, BaseView view);
        public abstract void Close();
        public abstract bool CanAcceptWidget<TWidget>() where TWidget : IWidget;
    }
}