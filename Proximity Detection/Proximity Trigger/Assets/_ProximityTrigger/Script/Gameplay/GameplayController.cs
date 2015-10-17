using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private Transform playerSpawPositionRoot;
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject playerClone = Instantiate(playerPrefab);
        playerClone.transform.SetParent(this.transform);

        Transform randomSpawnPosition = GetRandomSpawPoint();
        playerClone.transform.position = randomSpawnPosition.position;
    }

    private Transform GetRandomSpawPoint()
    {
        Transform[] availableSpawnPosition = playerSpawPositionRoot.GetComponentsInChildren<Transform>();
        return availableSpawnPosition[Random.Range(0, availableSpawnPosition.Length)];
    }
}
