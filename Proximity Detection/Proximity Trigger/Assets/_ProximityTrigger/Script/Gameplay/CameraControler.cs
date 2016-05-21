using Gameplay.Unit;
using UnityEngine;

namespace Gameplay
{
    public class CameraControler : MonoBehaviour
    {
        [SerializeField]
        private Vector3 playerOffset;
        [SerializeField]
        private float smoothTime = 0.3f;
        private Vector3 velocity = Vector3.zero;

        private Transform playerTransform;
        private GameplayController gameplayController;

        private void Awake()
        {
            gameplayController = GameplayController.Instance;
        }

        private void OnEnable()
        {
            gameplayController.OnPlayerSpawnEvent += OnPlayerSpawn;
        }
        private void OnDisable()
        {
            gameplayController.OnPlayerSpawnEvent -= OnPlayerSpawn;
        }

        private void OnPlayerSpawn(PlayerUnit player)
        {
            playerTransform = player.transform;
        }

        private void LateUpdate()
        {
            if(playerTransform == null)
                return;

            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            Vector3 finalPosition = playerTransform.position - playerOffset;
            transform.position = Vector3.SmoothDamp(transform.position, finalPosition,
                ref velocity, smoothTime);
        }
    }
}
