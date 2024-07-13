using Examples.Playground.Scripts.Screens;
using Smoothie;
using Smoothie.Scripts;
using UnityEngine;

namespace Playground
{
    public class InitPlayground : MonoBehaviour
    {
        [SerializeField] private SmoothieInstance _smoothieInstance;

        private readonly PlaygroundScreenViewModel _model = new();

        private void Start()
        {
            _smoothieInstance.Init();
            _smoothieInstance.Open<PlaygroundUiScreen>(_model);
        }

        private void OnDestroy()
        {
            _smoothieInstance.Close<PlaygroundUiScreen>(_model);
            _smoothieInstance.Terminate();
        }
    }
}