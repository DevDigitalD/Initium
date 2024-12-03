using System.Collections.Generic;
using UnityEngine;

namespace GameCore.ObjectPoolSystem
{
    public class ObjectPoolManager : MonoBehaviour, IObjectPoolManager
    {
        private const string OBJECT_POOL_CONFIG_PATH = "GameConfig/ObjectPool/ObjectPoolConfig";
        private Dictionary<string, Queue<GameObject>> _poolDictionary;
        private ObjectPoolConfig _objectPoolConfig;
    
        // [System.Serializable]
        // public class Pool
        // {
        //     public string name;
        //     public GameObject prefab;
        //     public int size;
        // }
        //
        // public List<Pool> pools;

        public void Init()
        {
            _objectPoolConfig = Resources.Load<ObjectPoolConfig>(OBJECT_POOL_CONFIG_PATH);
        }
        
        private void Start()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (var pool in _objectPoolConfig.Samples)
            {
                var objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    var obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool.name, objectPool);
            }
        }

        public GameObject GetFromPool(string label, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(label))
            {
                Debug.LogWarning("Pool with tag " + label + " doesn't exist.");
                return null;
            }

            var objectToReuse = _poolDictionary[label].Dequeue();

            objectToReuse.SetActive(true);
            objectToReuse.transform.position = position;
            objectToReuse.transform.rotation = rotation;

            _poolDictionary[label].Enqueue(objectToReuse);

            return objectToReuse;
        }
        
        public void ReturnToPool(GameObject objectToReturn)
        {
            objectToReturn.transform.parent = null;
            objectToReturn.SetActive(false);
        }

        public void Release()
        {
            
        }
    }
}