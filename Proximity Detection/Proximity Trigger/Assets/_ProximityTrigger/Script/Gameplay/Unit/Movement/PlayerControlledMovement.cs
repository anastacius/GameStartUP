using System;
using UnityEngine;
using System.Collections;
using Gameplay.Attribute;
using Gameplay.Unit.Movement;

namespace Gameplay.Unit.Movement
{
    [RequireComponent(typeof(NewBaseUnit))]
    public class PlayerControlledMovement : BaseMovement
    {
        private Vector3 playerInput = Vector3.zero;

        private void CheckInput()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            playerInput = new Vector3(horizontalInput, 0, verticalInput);
        }

        private void FixedUpdate()
        {
            CheckInput();
            Vector3 finalSpeed = playerInput*moveSpeedValue*Time.fixedDeltaTime;

            navMeshAgent.Move(finalSpeed);
        }
    }
}

