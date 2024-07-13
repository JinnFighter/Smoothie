using System.Collections.Generic;
using Smoothie.Scripts.Widgets;
using UnityEngine;

namespace Smoothie.Scripts.Layers
{
    public class ScreenLayer : BaseUiLayer
    {
        [SerializeField] private Canvas _layerCanvas;

        private readonly Dictionary<IViewModel, WidgetReference> _openedReferences = new();

        public override WidgetReference Open<TWidget>(IViewModel model, TWidget widget, BaseView view)
        {
            var widgetReference = new WidgetReference
            {
                Model = model,
                Widget = widget,
                View = view
            };
            _openedReferences[model] = widgetReference;
            view.transform.SetParent(_layerCanvas.transform, false);
            return widgetReference;
        }

        public override WidgetReference Close(IViewModel model)
        {
            var widgetReference = _openedReferences[model];
            _openedReferences.Remove(model);
            return widgetReference;
        }

        public override bool CanAcceptWidget(IViewModel model)
        {
            return !_openedReferences.ContainsKey(model);
        }

        public override bool ContainsWidget(IViewModel model)
        {
            return _openedReferences.ContainsKey(model);
        }
    }
}