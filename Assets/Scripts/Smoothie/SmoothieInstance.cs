using Smoothie.Pooling;
using UnityEngine;

namespace Smoothie
{
    public class SmoothieInstance : MonoBehaviour
    {
        [SerializeField] private SmoothieConfig _config;
        [SerializeField] private BasePoolProvider _poolProvider;
        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Init()
        {
            if (IsInitialized)
            {
                Debug.Log("Smoothie is already initialized, no need to initialize it");
                return;
            }

            _poolProvider.Init(_config);
            IsInitialized = true;
            Debug.Log("Smoothie successfully initialized");
        }

        public void Terminate()
        {
            if (!IsInitialized)
            {
                Debug.Log("Smoothie is not initialized, no need to terminate it");
                return;
            }

            _poolProvider.Terminate();
            IsInitialized = false;
            Debug.Log("Smoothie successfully terminated");
        }
    }
}