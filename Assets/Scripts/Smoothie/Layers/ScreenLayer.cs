using UnityEngine;

namespace Smoothie.Layers
{
    public class ScreenLayer : BaseUiLayer
    {
        [SerializeField] private Canvas _layerCanvas;

        public Transform LayerTransform => _layerCanvas.transform;
        public override void Open()
        {
        }

        public override void Close()
        {
        }

        public override bool CanAcceptWidget<TWidget>()
        {
            return true;
        }
    }
}