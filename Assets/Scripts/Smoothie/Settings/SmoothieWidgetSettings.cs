using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smoothie.Settings
{
    [CreateAssetMenu(menuName = "Smoothie/Create Widget Settings", fileName = "Create Widget Settings")]
    public class SmoothieWidgetSettings : ScriptableObject
    {
        [field: SerializeField] public List<SmoothieWidgetSetting> WidgetSettings { get; private set; }
    }

    [Serializable]
    public class SmoothieWidgetSetting
    {
        [field: SerializeField] public string WidgetType { get; private set; }
        [field: SerializeField] public ViewItemConfig ViewItemConfig { get; private set; }
    }
}