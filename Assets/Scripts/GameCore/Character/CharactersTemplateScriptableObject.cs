using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Character
{
    [CreateAssetMenu(menuName = "Configs/Characters/Template", fileName = "New Characters Template")]
    public class CharactersTemplateScriptableObject : ScriptableObject
    {
        [Header("Template"), TableList(AlwaysExpanded = true, DrawScrollView = false)] 
        public List<CharacterTemplate> CharacterTemplates;
    }

    [Serializable]
    public class CharacterTemplate
    {
        [TableColumnWidth(60, Resizable = false)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        public GameObject Prefab;
        [TableColumnWidth(60)]
        public CharactersType Type;
        [TableColumnWidth(20, Resizable = false)]
        public int Health;
        [TableColumnWidth(20, Resizable = false)]
        public int Damage;
        [TableColumnWidth(20, Resizable = false)]
        public int Armor;
    }
}
