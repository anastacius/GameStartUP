using UnityEngine;
using Gameplay.Attribute;
using Gameplay.Unit.Movement;


namespace Gameplay.Unit
{
    [RequireComponent(typeof(BaseMovement))]
    public class BaseUnit : MonoBehaviour
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

            baseMovement.Initialize();
        }
    }


}
