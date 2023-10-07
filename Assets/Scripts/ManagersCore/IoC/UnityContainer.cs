using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ManagersCore.IoC
{
    public class UnityContainer
    {
        private static Dictionary<Type, ObjectContainer> _dict = new Dictionary<Type, ObjectContainer>();

        public static T Resolve<T>()
        {
            if (_dict.TryGetValue(typeof(T), out ObjectContainer objectRef))
            {
                return (T)objectRef.Object;
            }

            foreach (ObjectContainer container in _dict.Values)
            {
                if (container.Object is T containerObject)
                {
                    return containerObject;
                }
            }
         
            Debug.LogWarning("There is no object of type " + typeof(T));
            return default;
        }

        public static void Bind<T>(object realObj, bool dontDestroy = false)
        {
            if (!_dict.ContainsKey(typeof(T)))
            {
                _dict.Add(typeof(T), new ObjectContainer(realObj, dontDestroy));
            }
            else
            {
                Debug.LogWarning("There is already realized type for " + typeof(T));
            } 
        }

        public static void Bind(object objectRef, Type type, bool dontDestroy = false)
        {
            if (!_dict.ContainsKey(type))
            {
                _dict.Add(type, new ObjectContainer(objectRef, dontDestroy));
            }
            else
            {
                Debug.LogWarning("There is already realized type for " + type);
            } 
        }

        public static void Clean() 
        { 
            var keysToClean = _dict.Where(x => !x.Value.DontDestroyable)
                .Select(x => x.Key);

            foreach (var key in keysToClean.ToArray()) 
            {
                _dict.Remove(key);
            }

            //_dict?.Clear();
        } 
    
        private class ObjectContainer 
        { 
            public object Object;
            public bool DontDestroyable;

            public ObjectContainer(object obj, bool dontDestroyable)
            {
                Object = obj;
                DontDestroyable = dontDestroyable;
            }
        }
    }
}