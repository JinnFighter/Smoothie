using System;
using System.Collections.Generic;
using System.Linq;
using Smoothie.Layers;
using Smoothie.Pooling;
using Smoothie.Settings;
using Smoothie.Widgets;
using UnityEngine;

namespace Smoothie
{
    public class SmoothieInstance : MonoBehaviour
    {
        [SerializeField] private SmoothieConfig _config;
        [SerializeField] private BasePoolProvider _poolProvider;
        [SerializeField] private ScreenLayer _screenLayer;
        [SerializeField] private SmoothieWidgetSettings _widgetSettings;

        private readonly Dictionary<IViewModel, (IWidget widget, BaseView view)> _widgets = new();

        private readonly Dictionary<string, Type> _viewTypes = new();
        private readonly Dictionary<string, Type> _viewFullTypes = new();
        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Init()
        {
            if (IsInitialized)
            {
                Debug.Log("Smoothie is already initialized, no need to initialize it");
                return;
            }

            _poolProvider.Init(_config);
            foreach (var item in _config.ViewItems)
            {
                _viewFullTypes[item.View.GetType().Name] = item.View.GetType();
            }
            foreach (var layer in _widgetSettings.LayerWidgetSettings)
            {
                foreach (var widgetSetting in layer.WidgetSettings)
                {
                    _viewTypes[widgetSetting.WidgetType] = _viewFullTypes[widgetSetting.ViewItemType];
                }
            }
            IsInitialized = true;
            Debug.Log("Smoothie successfully initialized");
        }

        public void Terminate()
        {
            if (!IsInitialized)
            {
                Debug.Log("Smoothie is not initialized, no need to terminate it");
                return;
            }
            
            _viewFullTypes.Clear();
            _viewTypes.Clear();

            var models = _widgets.Keys.ToList();
            foreach (var viewModel in models)
            {
                Close(viewModel);
            }
            
            _widgets.Clear();

            _poolProvider.Terminate();
            IsInitialized = false;
            Debug.Log("Smoothie successfully terminated");
        }
        
        public void Open<TWidget>(IViewModel model) where TWidget : IWidget
        {
            //1. Get widget type
            //2. Get layer by widget type (requires pre-saved layer)
            //3. Check if layer can accept widget with this type
            //4. If layer can accept widget with this type -> proceed, else -> do nothing
            //5. Get view type from pre-saved viewTypes (needs widgetType)
            //6. Create view from pool
            //7. Create widget from widget type (TODO: think about pooling widget logic classes)
            //8. Handle adding widget to layer
            var widgetType = typeof(TWidget);
            var viewType = _viewTypes[widgetType.Name];
            var view = _poolProvider.Get(viewType);
            view.transform.SetParent(_screenLayer.LayerTransform, false);
            var widget = Activator.CreateInstance<TWidget>();
            widget.Setup(model, view);
            widget.Init();
            _widgets[model] = (widget, view);
        }

        public void Close(IViewModel model)
        {
            _widgets[model].widget.Terminate();
            _poolProvider.Release(_widgets[model].view);
            _widgets.Remove(model);
        }
    }
}