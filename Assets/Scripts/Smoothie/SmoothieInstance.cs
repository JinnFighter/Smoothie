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
        private readonly Dictionary<string, BaseUiLayer> _layerObjects = new();
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
                    _layerObjects[widgetSetting.WidgetType] = layer.LayerObject;
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
            _layerObjects.Clear();

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
            var widgetType = typeof(TWidget);
            var layerObject = _layerObjects[widgetType.Name];
            var canLayerAcceptWidget = layerObject.CanAcceptWidget<TWidget>();
            if (!canLayerAcceptWidget)
            {
                return;
            }
            
            var widget = Activator.CreateInstance<TWidget>();
            var viewType = _viewTypes[widgetType.Name];
            var view = _poolProvider.Get(viewType);
            widget.Setup(model, view);
            layerObject.Open(model, widget, view);
            _widgets[model] = (widget, view);
            widget.Init();
        }

        public void Close(IViewModel model)
        {
            _widgets[model].widget.Terminate();
            _poolProvider.Release(_widgets[model].view);
            _widgets.Remove(model);
        }
    }
}