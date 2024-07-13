using System;
using System.Collections.Generic;
using Smoothie.Scripts.Widgets;
using UnityEngine;

namespace Smoothie.Scripts
{
    [Serializable]
    public class SmoothieConfig
    {
        [field: SerializeField] public List<ViewItemConfig> ViewItems { get; private set; }
    }

    [Serializable]
    public class ViewItemConfig
    {
        [field: SerializeField] public BaseView View { get; private set; }
        [field: SerializeField] public bool NeedPrewarm { get; private set; } = true;
        [field: SerializeField] public int PrewarmCount { get; private set; } = 1;
    }
}