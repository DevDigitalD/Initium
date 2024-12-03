using UnityEngine;
using UnityEngine.AI;

namespace GameCore.Character.EnemyCharacter
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _lookRadius = 10f; // Detection range for player
        [SerializeField] private Transform _target;

        //private Transform _target; // Reference to the player
        private NavMeshAgent _agent; // Reference to the NavMeshAgent
        private CharacterCombat _combat;

        // Use this for initialization
        private void Start()
        {
            //_target = PlayerManager.instance.player.transform;
            _agent = GetComponent<NavMeshAgent>();
            _combat = GetComponent<CharacterCombat>();
        }

        // Update is called once per frame
        private void Update()
        {
            // Distance to the target
            var distance = Vector3.Distance(_target.position, transform.position);

            // If inside the lookRadius
            if (!(distance <= _lookRadius)) 
                return;
            // Move towards the target
            _agent.SetDestination(_target.position);

            // If within attacking distance
            if (!(distance <= _agent.stoppingDistance)) 
                return;
            
            var targetStats = _target.GetComponent<CharacterStats>();
            
            if (targetStats != null) 
                _combat.Attack(targetStats);

            FaceTarget(); // Make sure to face towards the target
        }

        // Rotate to face the target
        private void FaceTarget()
        {
            var direction = (_target.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Show the lookRadius in editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _lookRadius);
        }
    }
}