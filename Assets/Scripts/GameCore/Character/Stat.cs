using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Character
{
    [CreateAssetMenu(menuName = "Configs/Character/Stats", fileName = "New Stats")]
    public class Stat : ScriptableObject
    {
        [SerializeField] private int _life;
        [SerializeField] private int _damage;
        [SerializeField] private int _armor;

        public int Life => _life;
        public int Damage => _damage;
        public int Armor => _armor;

        #region Old

        [SerializeField]
        private int baseValue;	// Starting value

        // List of modifiers that change the baseValue
        private readonly List<int> _modifiers = new();

        // Get the final value after applying modifiers
        public int GetValue ()
        {
            int finalValue = baseValue;
            _modifiers.ForEach(x => finalValue += x);
            return finalValue;
        }

        // Add new modifier
        public void AddModifier (int modifier)
        {
            if (modifier != 0)
                _modifiers.Add(modifier);
        }

        // Remove a modifier
        public void RemoveModifier (int modifier)
        {
            if (modifier != 0)
                _modifiers.Remove(modifier);
        }

        #endregion
    }
}
