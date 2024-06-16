using System.Collections.Generic;
using Smoothie.Widgets;
using UnityEngine;

namespace Smoothie
{
    [CreateAssetMenu(menuName = "Smoothie/Create Config", fileName = "SmoothieConfig")]
    public class SmoothieConfig : ScriptableObject
    {
        [field: SerializeField] public List<ViewItemConfig> ViewItems { get; private set; }
    }

    public class ViewItemConfig : ScriptableObject
    {
        [field: SerializeField] public BaseView View { get; private set; }
        [field: SerializeField] public bool NeedPrewarm { get; private set; } = true;
        [field: SerializeField] public int PrewarmCount { get; private set; } = 1;
    }
}