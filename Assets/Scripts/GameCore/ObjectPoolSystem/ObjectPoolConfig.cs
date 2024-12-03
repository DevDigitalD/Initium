using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameCore.ObjectPoolSystem
{
    [CreateAssetMenu(fileName = "ObjectPoolConfig", menuName = "ObjectPoolConfig", order = 0)]
    public class ObjectPoolConfig : ScriptableObject
    {
        [TableList(ShowIndexLabels = true, AlwaysExpanded = true, DrawScrollView = false)]
        public List<ObjectPoolSample> Samples;
    }

    [Serializable]
    public class ObjectPoolSample
    {
        [TableColumnWidth(60)]
        public string name;
        [TableColumnWidth(60)]
        public int size;
        [TableColumnWidth(60, Resizable = false)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        public GameObject prefab;
    }
}