using UnityEngine;
using System.Collections;
using Gameplay.Attribute;
using Gameplay.Unit.Movement;


namespace Gameplay.Unit
{
    [RequireComponent(typeof(BaseMovement))]
    public class NewBaseUnit : MonoBehaviour
    {
        private AttributePool attributePool;
        private BaseMovement baseMovement;


        public AttributePool AttributePool
        {
            get { return attributePool; }
        }


        private void Awake()
        {
            baseMovement = GetComponent<BaseMovement>();
            attributePool = GetComponentInChildren<AttributePool>();
        }


        private void Start()
        {
            attributePool.GetAttribute(AttributeType.MoveSpeed).Initialize(5, 10);

            baseMovement.Initialize();
        }
    }


}
