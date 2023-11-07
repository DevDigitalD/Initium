using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameCore.Character
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombat : MonoBehaviour
    {
        public event System.Action OnAttack;
        
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private float _attackDelay = 1f;
        
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
                StartCoroutine(DoDamage(targetStats, _attackDelay));

                if (OnAttack != null)
                    OnAttack();

                _attackCooldown = 1f / _attackSpeed;
            }
        }

        IEnumerator DoDamage(CharacterStats stats, float delay)
        {
            yield return new WaitForSeconds(delay);

            //stats.TakeDamage(_myStats.Damage);
        }
    }
}