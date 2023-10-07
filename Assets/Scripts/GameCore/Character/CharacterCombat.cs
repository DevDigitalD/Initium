using System.Collections;
using UnityEngine;

namespace GameCore.Character
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombat : MonoBehaviour
    {
        public event System.Action OnAttack;
        
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private float attackDelay = .6f;
        
        private float _attackCooldown = 0f;
        private CharacterStats _myStats;

        private void Start()
        {
            _myStats = GetComponent<CharacterStats>();
        }

        private void Update()
        {
            _attackCooldown -= Time.deltaTime;
        }

        public void Attack(CharacterStats targetStats)
        {
            if (_attackCooldown <= 0f)
            {
                StartCoroutine(DoDamage(targetStats, attackDelay));

                if (OnAttack != null)
                    OnAttack();

                _attackCooldown = 1f / _attackSpeed;
            }
        }

        IEnumerator DoDamage(CharacterStats stats, float delay)
        {
            yield return new WaitForSeconds(delay);

            stats.TakeDamage(_myStats.damage.GetValue());
        }
    }
}