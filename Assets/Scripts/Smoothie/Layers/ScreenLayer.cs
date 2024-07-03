using UnityEngine;

namespace Smoothie.Layers
{
    public class ScreenLayer : MonoBehaviour
    {
        [SerializeField] private Canvas _layerCanvas;

        public Transform LayerTransform => _layerCanvas.transform;
    }
}