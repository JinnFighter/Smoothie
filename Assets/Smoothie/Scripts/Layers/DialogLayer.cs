using Smoothie.Scripts.Widgets;
using UnityEngine;

namespace Smoothie.Scripts.Layers
{
    public class DialogLayer : BaseUiLayer
    {
        [SerializeField] private Canvas _layerCanvas;
        [SerializeField] private GameObject _backgroundFader;

        private WidgetReference _currentDialog;

        public override WidgetReference Open<TWidget>(IViewModel model, TWidget widget, BaseView view)
        {
            var widgetReference = new WidgetReference
            {
                Model = model,
                Widget = widget,
                View = view
            };
            _currentDialog = widgetReference;
            view.transform.SetParent(_layerCanvas.transform, false);
            _backgroundFader.SetActive(true);
            return widgetReference;
        }

        public override WidgetReference Close(IViewModel model)
        {
            var widgetReference = _currentDialog;
            _currentDialog = null;
            _backgroundFader.SetActive(false);
            return widgetReference;
        }

        public override bool CanAcceptWidget(IViewModel model)
        {
            return _currentDialog == null;
        }

        public override bool ContainsWidget(IViewModel model)
        {
            return _currentDialog != null;
        }
    }
}