using System.Collections;
using Gameplay.Unit.Attack;
using Gameplay.Unit.Movement;
using UnityEngine;

namespace Gameplay.Unit
{
    [RequireComponent(typeof (PathAgentController))]
    public class BaseEnemy : BaseUnit
    {
        public delegate void BaseEnemyDelegate(BaseEnemy enemy);

        public event BaseEnemyDelegate EnemyDieEvent;


        [SerializeField]
        private TriggerVolume sightTriggerVolume;

        private Rigidbody rigidbody;
        private PlayerUnit currentTarget;
        private PathAgentController pathAgentController;
        private BehaviorState state = BehaviorState.Idle;
        private Coroutine pushRoutine;

        protected override void Awake()
        {
            base.Awake();
            pathAgentController = GetComponent<PathAgentController>();
            rigidbody = GetComponent<Rigidbody>();

            pathAgentController.OnReachDestination += OnReachDestination;

            sightTriggerVolume.OnTriggerEnterEvent += OnSightTriggerVolumeEnter;
            sightTriggerVolume.OnTriggerExitEvent += OnSightTriggerVolumeExit;
        }

        protected override void Start()
        {
            base.Start();
            ChangeStateTo(BehaviorState.Patrolling);
        }

        private void OnDestroy()
        {
            pathAgentController.OnReachDestination -= OnReachDestination;

            sightTriggerVolume.OnTriggerEnterEvent -= OnSightTriggerVolumeEnter;
            sightTriggerVolume.OnTriggerExitEvent -= OnSightTriggerVolumeExit;
        }

        private void OnSightTriggerVolumeExit(TriggerVolume volume, Collider collider)
        {
            currentTarget = null;
            ChangeStateTo(BehaviorState.Patrolling);
        }

        private void OnSightTriggerVolumeEnter(TriggerVolume volume, Collider collider)
        {
            currentTarget = collider.GetComponent<PlayerUnit>();
            ChangeStateTo(BehaviorState.SeekingTarget);
        }

        public void ChangeStateTo(BehaviorState targetState)
        {
            if (state == BehaviorState.Idle && targetState == BehaviorState.Patrolling)
            {
                SeekNewPosition();
            }
            else if (state == BehaviorState.Patrolling && targetState == BehaviorState.Attacking)
            {
                pathAgentController.Stop();
            }
            state = targetState;
        }

        private void SeekNewPosition()
        {
            Vector3 randomPosition = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            pathAgentController.SetDestination(randomPosition);
        }

        private void OnReachDestination(Vector3 startPosition, Vector3 endPosition)
        {
            if (state == BehaviorState.Patrolling)
                SeekNewPosition();
        }

        public override void Hit(HitInformation hitInformation)
        {
            base.Hit(hitInformation);

            if(!gameObject.activeInHierarchy)
                return;

            if (pushRoutine != null)
            {
                StopCoroutine(pushRoutine);
                pushRoutine = null;
            }
            pushRoutine = StartCoroutine(PushBackRoutine(hitInformation));
        }

        private IEnumerator PushBackRoutine(HitInformation hitInformation)
        {
            Vector3 direction = hitInformation.HitPosition - hitInformation.Shooter.transform.position;
            rigidbody.isKinematic = false;
            rigidbody.AddForce(direction.normalized* 30, ForceMode.Impulse);
            yield return new WaitForSeconds(0.05f);
            rigidbody.isKinematic = true;
       }

        protected override void Die()
        {
            base.Die();
            SimplePool.Despawn(this.gameObject);

            DispatchEnemyDie();
        }

        private void DispatchEnemyDie()
        {
            if (EnemyDieEvent != null)
                EnemyDieEvent(this);
        }

        private void Update()
        {
            if (state == BehaviorState.SeekingTarget)
            {
                pathAgentController.SetDestination(currentTarget.transform.position);
            }
        }

       
    }
}
