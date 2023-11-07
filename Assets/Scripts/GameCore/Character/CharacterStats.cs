using UnityEngine;
using UnityEngine.Serialization;

namespace GameCore.Character
{
    public sealed class CharacterStats : MonoBehaviour
    {
        //public int Damage => enemyCharactersTemplateScriptableOblect.Damage;

        [FormerlySerializedAs("enemyCharactersTemplate")] [FormerlySerializedAs("enemyCharacterStat")] [FormerlySerializedAs("_enemyStat")] [SerializeField] private CharactersTemplateScriptableObject enemyCharactersTemplateScriptableOblect;
        
        private int _currentHealth;

        // Set current health to max health
        // when starting the game.
        private void Start ()
        {
            //_currentHealth = enemyCharactersTemplateScriptableOblect.Health;
        }

        // Damage the character
        public void TakeDamage (int damage)
        {
            // Subtract the armor value
            //damage -= enemyCharactersTemplateScriptableOblect.Armor;
            damage = Mathf.Clamp(damage, 0, int.MaxValue);

            // Damage the character
            _currentHealth -= damage;
            Debug.Log(transform.name + " takes " + damage + " damage.");

            // If health reaches zero
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die ()
        {
            // Die in some way
            // This method is meant to be overwritten
            Debug.Log(transform.name + " died.");
        }
    }
}
