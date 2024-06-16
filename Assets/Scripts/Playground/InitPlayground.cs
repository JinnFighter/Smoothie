using Smoothie;
using UnityEngine;

namespace Playground
{
    public class InitPlayground : MonoBehaviour
    {
        [SerializeField] private SmoothieInstance _smoothieInstance;

        private void Start()
        {
            _smoothieInstance.Init();
        }

        private void OnDestroy()
        {
            _smoothieInstance.Terminate();
        }
    }
}