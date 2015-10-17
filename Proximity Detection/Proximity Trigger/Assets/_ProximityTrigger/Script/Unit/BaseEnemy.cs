using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgentController))]
public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgentController navMeshAgentController;

    private void Awake()
    {
        navMeshAgentController = GetComponent<NavMeshAgentController>();
    }

    private void Start()
    {
        SeekNewPosition();
    }

    private void OnEnable()
    {
        navMeshAgentController.OnReachDestination += SeekNewPosition;
    }

    private void OnDisable()
    {
        navMeshAgentController.OnReachDestination -= SeekNewPosition;
    }

    private void SeekNewPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-50,50), 0 , Random.Range(-50, 50));
        navMeshAgentController.SetDestination(randomPosition);
    }
}
