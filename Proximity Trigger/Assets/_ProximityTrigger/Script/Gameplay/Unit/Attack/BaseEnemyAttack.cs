using UnityEngine;
using System.Collections;
using Gameplay.Attribute;

namespace Gameplay.Unit.Attack
{
    public class BaseEnemyAttack : MonoBehaviour
    {

        [SerializeField]
        private TriggerVolume attackTriggerVolume;
        [SerializeField]
        private float attackCooldown = 0.7f;
        [SerializeField]
        private float damage;

        private BaseEnemy enemy;
        private float attackTime;
        private Attribute.Attribute playerHealth;

        private void Awake()
        {
            enemy = GetComponent<BaseEnemy>();
            attackTriggerVolume.OnTriggerStayEvent += OnAttackTriggerVolumeStay;
            attackTriggerVolume.OnTriggerExitEvent += OnAttackTriggerVolumeExit;
        }

      

        private void OnDestroy()
        {
            attackTriggerVolume.OnTriggerStayEvent -= OnAttackTriggerVolumeStay;
            attackTriggerVolume.OnTriggerExitEvent -= OnAttackTriggerVolumeExit;
        }

        private void OnAttackTriggerVolumeStay(TriggerVolume volume, Collider collider1)
        {
            PlayerUnit playerUnit = collider1.GetComponent<PlayerUnit>();
            playerHealth = playerUnit.AttributePool.GetAttribute(AttributeType.Health);
            ExecuteAttack();
        }

        private void ExecuteAttack()
        {
            if(Time.time < attackTime)
                return;

            attackTime = Time.time + attackCooldown;

            playerHealth.ChangeValue(-damage);
        }

        private void OnAttackTriggerVolumeExit(TriggerVolume volume, Collider collider1)
        {
            enemy.ChangeStateTo(BehaviorState.Patrolling);
        }
    }
}
