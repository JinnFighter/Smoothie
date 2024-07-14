using Smoothie.Scripts.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Playground.Scripts.Dialogs
{
    public class PlaygroundDialogView : BaseView
    {
        [field: SerializeField] public Button ButtonAccept { get; private set; }
    }
}