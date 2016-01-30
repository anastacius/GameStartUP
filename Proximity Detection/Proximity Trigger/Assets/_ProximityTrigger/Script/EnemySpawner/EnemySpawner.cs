using System.Collections;
using System.Collections.Generic;
using Gameplay.Unit;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float minimumSpawnNewEnemyInterval = 5.0f;
    [SerializeField]
    private float maximumSpawnNewEnemyInterval = 15.0f;

    
    [SerializeField]
    private BaseEnemy enemyPrefab;
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
            SpawNewEnemy();
            yield return new WaitForSeconds(Random.Range(minimumSpawnNewEnemyInterval, maximumSpawnNewEnemyInterval));
        }
    }

    private void SpawNewEnemy()
    {
        BaseEnemy enemyClone = SimplePool.Spawn(enemyPrefab.gameObject).GetComponent<BaseEnemy>();
        enemyClone.Initialize();
        enemyClone.transform.SetParent(transform);
        enemyClone.transform.localPosition = Vector3.zero;
    }
}
