using UnityEngine;

namespace GameCore.Character
{
    public sealed class CharacterStats : MonoBehaviour
    {
        public int Damage => _enemyStat.Damage;

        [SerializeField] private Stat _enemyStat;
        
        private int _currentHealth;

        // Set current health to max health
        // when starting the game.
        private void Start ()
        {
            _currentHealth = _enemyStat.Life;
        }

        // Damage the character
        public void TakeDamage (int damage)
        {
            // Subtract the armor value
            damage -= _enemyStat.Armor;
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
