using System;
using System.Collections.Generic;
using Smoothie.Scripts.Widgets;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smoothie.Scripts.Pooling
{
    public class DefaultPoolProvider : BasePoolProvider
    {
        [SerializeField] private Transform _poolParent;

        private readonly Dictionary<Type, Queue<BaseView>> _pooledItems = new();

        public override void Init(SmoothieConfig config)
        {
            foreach (var viewItemConfig in config.ViewItems)
            {
                if (!viewItemConfig.NeedPrewarm) continue;

                PrewarmPool(viewItemConfig);
            }
            
            Debug.Log("Default Pool Provider Initialized");
        }

        public override void Terminate()
        {
            foreach (var type in _pooledItems.Keys)
            {
                while (_pooledItems[type].Count > 0)
                {
                    var view = _pooledItems[type].Dequeue();
                    if (view == null)
                    {
                        continue;
                    }
                    
                    Object.Destroy(view.gameObject);
                }
            }
            
            _pooledItems.Clear();
            
            Debug.Log("Default Pool Provider Terminated");
        }

        public override T Get<T>()
        {
            if (!_pooledItems.TryGetValue(typeof(T), out var items))
                throw new Exception($"Pool size limit reached for object of type : {typeof(T)}");

            var res = items.Dequeue();
            res.transform.SetParent(null, false);
            res.gameObject.SetActive(true);
            return (T)res;
        }

        public override BaseView Get(Type type)
        {
            if (!_pooledItems.TryGetValue(type, out var items))
                throw new Exception($"Pool size limit reached for object of type : {type}");

            var res = items.Dequeue();
            res.transform.SetParent(null, false);
            res.gameObject.SetActive(true);
            return res;
        }

        public override void Release<T>(T obj)
        {
            if (!_pooledItems.TryGetValue(obj.GetType(), out var items))
            {
                items = new Queue<BaseView>();
                _pooledItems.Add(obj.GetType(), items);
            }

            if (obj == null)
            {
                return;
            }
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_poolParent, false);
            items.Enqueue(obj);
        }

        private void PrewarmPool(ViewItemConfig viewItemConfig)
        {
            for (var i = 0; i < viewItemConfig.PrewarmCount; i++)
            {
                if (!_pooledItems.TryGetValue(viewItemConfig.View.GetType(), out var items))
                {
                    items = new Queue<BaseView>();
                    _pooledItems.Add(viewItemConfig.View.GetType(), items);
                }

                var view = Instantiate(viewItemConfig.View, _poolParent, false);
                view.gameObject.SetActive(false);
                items.Enqueue(view);
            }
        }
    }
}