using System;
using System.Collections.Generic;
using Smoothie.Layers;
using UnityEngine;

namespace Smoothie.Settings
{
    [Serializable]
    public class SmoothieWidgetSettings
    {
        [field: SerializeField] public List<LayerWidgetSettings> LayerWidgetSettings { get; private set; }
    }

    [Serializable]
    public class SmoothieWidgetSetting
    {
        [field: SerializeField] public string WidgetType { get; private set; }
        [field: SerializeField] public string ViewItemType { get; private set; }
    }

    [Serializable]
    public class LayerWidgetSettings
    {
        [field: SerializeField] public BaseUiLayer LayerObject { get; private set; }
        [field: SerializeField] public List<SmoothieWidgetSetting> WidgetSettings { get; private set; }
    }
}