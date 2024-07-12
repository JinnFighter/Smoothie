using Smoothie.Widgets;
using UnityEngine;

namespace Smoothie.Layers
{
    public abstract class BaseUiLayer : MonoBehaviour, IUiLayer
    {
        public abstract WidgetReference Open<TWidget>(IViewModel model, TWidget widget, BaseView view) where TWidget : IWidget;
        public abstract WidgetReference Close(IViewModel model);
        public abstract bool CanAcceptWidget(IViewModel model);
        public abstract bool ContainsWidget(IViewModel model);
    }
}