using UnityEngine;
using Gameplay.Unit.Movement;
using Random = UnityEngine.Random;

namespace Gameplay.Unit
{
    [RequireComponent(typeof (PathAgentController))]
    public class BaseEnemy : BaseUnit
    {
        [SerializeField]
        private TriggerVolume sightTriggerVolume;

        private PathAgentController pathAgentController;
        private BehaviorState state = BehaviorState.Idle;
        
        private PlayerUnit currentTarget;

        protected override void Awake()
        {
            base.Awake();
            pathAgentController = GetComponent<PathAgentController>();

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
            if(state == BehaviorState.Patrolling)
                SeekNewPosition();
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