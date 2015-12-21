using UnityEngine;
using Gameplay.Unit;

namespace Gameplay
{
    public class GameplayController : MonoSingleton<GameplayController>
    {
        public delegate void OnPlayerSpawnedDelegate(PlayerUnit player);
        public event OnPlayerSpawnedDelegate OnPlayerSpawnEvent;

        [SerializeField]
        private Transform playerSpawPositionRoot;
        [SerializeField]
        private GameObject playerPrefab;

        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            PlayerUnit playerClone = Instantiate(playerPrefab).GetComponent<PlayerUnit>();
            playerClone.transform.SetParent(this.transform);

            Transform randomSpawnPosition = GetRandomSpawPoint();
            playerClone.transform.position = randomSpawnPosition.position;

            DispatchOnPlayerSpawnEvent(playerClone);
        }

        private void DispatchOnPlayerSpawnEvent(PlayerUnit targetPlayer)
        {
            if (OnPlayerSpawnEvent != null)
                OnPlayerSpawnEvent(targetPlayer);
        }

        private Transform GetRandomSpawPoint()
        {
            Transform[] availableSpawnPosition = playerSpawPositionRoot.GetComponentsInChildren<Transform>();
            return availableSpawnPosition[Random.Range(0, availableSpawnPosition.Length)];
        }
    }
}