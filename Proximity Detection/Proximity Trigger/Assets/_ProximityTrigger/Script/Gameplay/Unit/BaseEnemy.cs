using UnityEngine;
using Gameplay.Unit.Movement;
using Random = UnityEngine.Random;

namespace Gameplay.Unit
{
    [RequireComponent(typeof (PathAgentController))]
    public class BaseEnemy : BaseUnit
    {
        private PathAgentController pathAgentController;

        protected override void Awake()
        {
            base.Awake();
            pathAgentController = GetComponent<PathAgentController>();
        }

        protected override void Start()
        {
            base.Start();
            SeekNewPosition();
        }

        private void OnEnable()
        {
            pathAgentController.OnReachDestination += OnReachDestination;
        }

        private void OnDisable()
        {
            pathAgentController.OnReachDestination -= OnReachDestination;
        }

        private void SeekNewPosition()
        {
            Vector3 randomPosition = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            pathAgentController.SetDestination(randomPosition);
        }

        private void OnReachDestination(Vector3 startPosition, Vector3 endPosition)
        {
            SeekNewPosition();
        }
    }
}