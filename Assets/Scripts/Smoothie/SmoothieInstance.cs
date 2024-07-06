using System;
using System.Collections.Generic;
using System.Linq;
using Smoothie.Layers;
using Smoothie.Pooling;
using Smoothie.Widgets;
using UnityEngine;

namespace Smoothie
{
    public class SmoothieInstance : MonoBehaviour
    {
        [SerializeField] private SmoothieConfig _config;
        [SerializeField] private BasePoolProvider _poolProvider;
        [SerializeField] private ScreenLayer _screenLayer;

        private readonly Dictionary<IViewModel, (IWidget widget, BaseView view)> _widgets = new();
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

        public void Open<TWidget>(IViewModel model, Type viewType) where TWidget : IWidget
        {
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