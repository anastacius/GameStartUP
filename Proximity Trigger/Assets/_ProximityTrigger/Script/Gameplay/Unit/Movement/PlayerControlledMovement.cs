using UnityEngine;

namespace Gameplay.Unit.Movement
{
    [RequireComponent(typeof(BaseUnit))]
    public class PlayerControlledMovement : BaseMovement
    {
        [SerializeField]
        private LayerMask groundLayer;

        private Vector3 playerInput = Vector3.zero;
        private Quaternion mouseRotation = Quaternion.identity;
        private float horizontalInput;
        private float verticalInput;

        private void CheckInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, 100, groundLayer.value))
            {
                Vector3 diff = hit.point - transform.position;
                diff.y = 0;

                mouseRotation = Quaternion.LookRotation(diff);
            }
        }

        private void Update()
        {
            if(!isLocalPlayer)
                return;

            CheckInput();
            Move();
            Turn();

        }

        private void Turn()
        {
            rigidBody.MoveRotation(mouseRotation);
        }

        private void Move()
        {
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
            movement = movement.normalized*moveSpeedValue*Time.deltaTime;
            navMeshAgent.Move(movement);
        }
    }
}

