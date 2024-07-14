using Smoothie.Scripts.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Playground.Scripts.Screens
{
    public class PlaygroundScreenView : BaseView
    {
        [field: SerializeField] public Button ButtonCreateDialog { get; private set; }
    }
}