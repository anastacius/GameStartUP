using System;
using UnityEngine;
using Gameplay.Unit.Movement;
using Random = UnityEngine.Random;

namespace Gameplay.Unit
{
    [RequireComponent(typeof (PathAgentController))]
    public class BaseEnemy : BaseUnit
    {
        private PathAgentController pathAgentController;
        private BehaviorState state = BehaviorState.Idle;
        private TriggerVolume triggerVolume;
        private PlayerUnit currentTarget;

        protected override void Awake()
        {
            base.Awake();
            pathAgentController = GetComponent<PathAgentController>();
            triggerVolume = GetComponentInChildren<TriggerVolume>();
        }

        protected override void Start()
        {
            base.Start();
            ChangeStateTo(BehaviorState.Patrolling);
        }

        private void OnEnable()
        {
            pathAgentController.OnReachDestination += OnReachDestination;
            triggerVolume.OnTriggerEnterEvent += OnTriggerVolumeEnter;
            triggerVolume.OnTriggerExitEvent += OnTriggerVolumeExit;
        }
        private void OnDisable()
        {
            pathAgentController.OnReachDestination -= OnReachDestination;
            triggerVolume.OnTriggerEnterEvent -= OnTriggerVolumeEnter;
            triggerVolume.OnTriggerExitEvent -= OnTriggerVolumeExit;
        }

        private void OnTriggerVolumeExit(TriggerVolume volume, Collider collider)
        {
            currentTarget = null;
            ChangeStateTo(BehaviorState.Patrolling);
        }

        private void OnTriggerVolumeEnter(TriggerVolume volume, Collider collider)
        {
            currentTarget = collider.GetComponent<PlayerUnit>();
            ChangeStateTo(BehaviorState.SeekingTarget);
        }

        private void ChangeStateTo(BehaviorState targetState)
        {
            if (state == BehaviorState.Idle && targetState == BehaviorState.Patrolling)
            {
                SeekNewPosition();
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