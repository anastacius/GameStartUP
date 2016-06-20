using Gameplay.Unit;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private BaseEnemy enemyPrefab;



    public BaseEnemy Spawn(int targetHealth, int targetMoveSpeed)
    {
        BaseEnemy enemyClone = SimplePool.Spawn(enemyPrefab.gameObject).GetComponent<BaseEnemy>();
        enemyClone.Initialize(targetHealth, targetMoveSpeed);
        enemyClone.transform.SetParent(transform);
        enemyClone.transform.localPosition = Vector3.zero;
        enemyClone.transform.localEulerAngles = Vector3.zero;
        return enemyClone;
    }
}
