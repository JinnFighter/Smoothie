using Smoothie.Widgets;
using UnityEngine;

namespace Smoothie.Layers
{
    public class ScreenLayer : BaseUiLayer
    {
        [SerializeField] private Canvas _layerCanvas;

        public override void Open<TWidget>(IViewModel model, TWidget widget, BaseView view)
        {
            view.transform.SetParent(_layerCanvas.transform, false);
        }

        public override void Close()
        {
        }

        public override bool CanAcceptWidget<TWidget>()
        {
            return true;
        }
    }
}