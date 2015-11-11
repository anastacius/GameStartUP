using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Unit;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnNewEnemyInterval = 5.0f;
    [SerializeField] private int maxEnemies = 10;

    [SerializeField] private BaseEnemy enemyPrefab;


    private List<BaseEnemy> aliveEnemies = new List<BaseEnemy>();
    private bool alive;

    private void Start()
    {
        StartCoroutine(SpawEnemyCoroutine());
    }

    private IEnumerator SpawEnemyCoroutine()
    {
        alive = true;
        while (alive)
        {
            if (CanSpawnMoreEnemies())
            {
                SpawNewEnemy();
            }
            yield return new WaitForSeconds(spawnNewEnemyInterval);
        }
    }

    private void SpawNewEnemy()
    {
        BaseEnemy enemyClone = Instantiate(enemyPrefab);
        enemyClone.transform.SetParent(this.transform);
        enemyClone.transform.localPosition = Vector3.zero;

        aliveEnemies.Add(enemyClone);
    }


    private bool CanSpawnMoreEnemies()
    {
        if (aliveEnemies.Count < maxEnemies)
            return true;

        return false;
    }
}
