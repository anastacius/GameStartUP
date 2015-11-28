using UnityEngine;
using Gameplay.Attribute;
using Gameplay.Unit.Attack;
using Gameplay.Unit.Movement;


namespace Gameplay.Unit
{
    [RequireComponent(typeof(BaseMovement))]
    public class BaseUnit : MonoBehaviour, IHitByBullet
    {
        private AttributePool attributePool;
        private BaseMovement baseMovement;

        public AttributePool AttributePool
        {
            get { return attributePool; }
        }


        protected virtual void Awake()
        {
            baseMovement = GetComponent<BaseMovement>();
            attributePool = GetComponentInChildren<AttributePool>();
        }


        protected virtual void Start()
        {
            attributePool.GetAttribute(AttributeType.MoveSpeed).Initialize(5, 10);
            attributePool.GetAttribute(AttributeType.Health).Initialize(100, 100);

            baseMovement.Initialize();
        }

        public void Hit(BaseWeapon baseWeapon)
        {
            attributePool.GetAttribute(AttributeType.Health).ChangeValue(-baseWeapon.GetWeaponDefinition().GetDamage());
        }
    }


}
