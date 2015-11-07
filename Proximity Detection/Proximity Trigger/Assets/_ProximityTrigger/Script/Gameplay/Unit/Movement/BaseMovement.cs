using System;
using UnityEngine;
using System.Collections;
using Gameplay.Attribute;
using UnityEditor;

namespace Gameplay.Unit.Movement
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
    public class BaseMovement : MonoBehaviour
    {
        protected NavMeshAgent navMeshAgent;
        protected Rigidbody rigidBody;
        protected NewBaseUnit baseUnit;

        protected float moveSpeedValue;


        private Attribute.Attribute moveSpeedAttribute;

        protected virtual void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            baseUnit = GetComponent<NewBaseUnit>();
        }

        private void OnDisable()
        {
            moveSpeedAttribute.OnAttributeChange -= OnMoveSpeedAttributeChange;
        }

        public void Initialize()
        {
            moveSpeedAttribute = baseUnit.AttributePool.GetAttribute(AttributeType.MoveSpeed);
            moveSpeedAttribute.OnAttributeChange += OnMoveSpeedAttributeChange;
            OnMoveSpeedAttributeChange(0, moveSpeedAttribute.CurrentValue);
        }

        private void OnMoveSpeedAttributeChange(float prevValue, float currentValue)
        {
            moveSpeedValue = currentValue;
        }
    }
}
