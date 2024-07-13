using System;
using System.Collections.Generic;
using Smoothie.Scripts.Layers;
using Smoothie.Scripts.Pooling;
using Smoothie.Scripts.Settings;
using Smoothie.Scripts.Widgets;
using UnityEngine;

namespace Smoothie.Scripts
{
    public class SmoothieInstance : MonoBehaviour, ISmoothieInstance
    {
        [SerializeField] private SmoothieConfig _config;
        [SerializeField] private BasePoolProvider _poolProvider;
        [SerializeField] private ScreenLayer _screenLayer;
        [SerializeField] private SmoothieWidgetSettings _widgetSettings;
        private readonly Dictionary<IViewModel, WidgetReference> _widgets = new();
        private readonly Dictionary<string, WidgetInfo> _widgetSetupInfo = new();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public bool IsInitialized { get; private set; }

        public void Init()
        {
            if (IsInitialized)
            {
                Debug.Log("Smoothie is already initialized, no need to initialize it");
                return;
            }

            _poolProvider.Init(_config);
            var viewFullTypes = new Dictionary<string, Type>();
            foreach (var item in _config.ViewItems) viewFullTypes[item.View.GetType().Name] = item.View.GetType();
            foreach (var layerSettings in _widgetSettings.LayerWidgetSettings)
            foreach (var widgetSetting in layerSettings.WidgetSettings)
            {
                var widgetInfo = new WidgetInfo
                {
                    ViewType = viewFullTypes[widgetSetting.ViewItemType],
                    LayerObject = layerSettings.LayerObject
                };
                _widgetSetupInfo[widgetSetting.WidgetType] = widgetInfo;
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

            foreach (var viewModel in _widgets.Keys) CloseInner(viewModel, _widgets[viewModel].Widget.GetType().Name);

            _poolProvider.Terminate();
            _widgetSetupInfo.Clear();

            IsInitialized = false;
            Debug.Log("Smoothie successfully terminated");
        }

        public void Open<TWidget>(IViewModel model) where TWidget : IWidget
        {
            var widgetType = typeof(TWidget).Name;
            var widgetSetupInfo = _widgetSetupInfo[widgetType];
            var layerObject = widgetSetupInfo.LayerObject;
            var canLayerAcceptWidget = layerObject.CanAcceptWidget(model);
            if (!canLayerAcceptWidget) return;

            var widget = Activator.CreateInstance<TWidget>();
            var viewType = widgetSetupInfo.ViewType;
            var view = _poolProvider.Get(viewType);
            widget.Setup(model, view);
            var widgetReference = layerObject.Open(model, widget, view);
            widget.Init();

            _widgets[model] = widgetReference;
        }

        public void Close<TWidget>(IViewModel model) where TWidget : IWidget
        {
            var widgetType = typeof(TWidget).Name;
            CloseInner(model, widgetType);
        }

        private void CloseInner(IViewModel model, string widgetType)
        {
            var widgetInfo = _widgetSetupInfo[widgetType];
            var layerObject = widgetInfo.LayerObject;
            if (!layerObject.ContainsWidget(model)) return;

            var widgetReference = layerObject.Close(model);
            widgetReference.Widget.Terminate();
            _poolProvider.Release(widgetReference.View);
        }
    }
}