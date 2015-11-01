using UnityEngine;
using Gameplay.Attribute;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgentController))]
public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgentController navMeshAgentController;
    private AttributePool attributePool;

    [SerializeField]
    private bool remove10Health;

    [SerializeField]
    private bool increaseArmorBy1;

    private void Awake()
    {
        navMeshAgentController = GetComponent<NavMeshAgentController>();
        attributePool = GetComponentInChildren<AttributePool>();
    }

    private void Start()
    {
        SeekNewPosition();

        attributePool.GetAttribute(AttributeType.Health).Initialize(100, 100);
        attributePool.GetAttribute(AttributeType.Armor).Initialize(2, 10);
        attributePool.GetAttribute(AttributeType.MoveSpeed).Initialize(4, 10);

        attributePool.GetAttribute(AttributeType.Health).OnAttributeOver += OnHealthOver;
    }

    private void OnEnable()
    {
        navMeshAgentController.OnReachDestination += SeekNewPosition;

        

    }

    

    private void OnDisable()
    {
        navMeshAgentController.OnReachDestination -= SeekNewPosition;

        attributePool.GetAttribute(AttributeType.Health).OnAttributeOver -= OnHealthOver;
    }

    private void OnHealthOver(float prevValue, float currentValue)
    {
        Destroy(this.gameObject);
    }

    private void SeekNewPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-50,50), 0 , Random.Range(-50, 50));
        navMeshAgentController.SetDestination(randomPosition);
    }

    private void Update()
    {
        if (remove10Health)
        {
            attributePool.GetAttribute(AttributeType.Health).ChangeValue(-10);
            remove10Health = false;
        }

        if (increaseArmorBy1)
        {
            attributePool.GetAttribute(AttributeType.Armor).ChangeValue(1);
            increaseArmorBy1 = false;
        }
    }
}
