using Gameplay.Score;
using UnityEngine;
using Gameplay.Unit;
using Gameplay.Unit.Attack;

namespace Gameplay
{
    public class GameplayController : MonoSingleton<GameplayController>
    {
        public delegate void EnemyDiedDelegate(HitInformation hitInformation);
        public delegate void VoidDelegate();
        public delegate void OnPlayerSpawnedDelegate(PlayerUnit player);

        public event EnemyDiedDelegate OnEnemyDiedEvent;
        public event OnPlayerSpawnedDelegate OnPlayerSpawnEvent;
        public event VoidDelegate GameStartedEvent;

        [SerializeField]
        private ScoreController scoreController;
        [SerializeField]
        private Transform playerSpawPositionRoot;
        [SerializeField]
        private GameObject playerPrefab;

        public ScoreController ScoreController
        {
            get { return scoreController; }
        }

       
        private void Start()
        {
            //SpawnPlayer();

            //DispatchGameStarted();
        }
        /*
        private void DispatchGameStarted()
        {
            if (GameStartedEvent != null)
                GameStartedEvent();

        }
        
        private void SpawnPlayer()
        {
            PlayerUnit playerClone = Instantiate(playerPrefab).GetComponent<PlayerUnit>();
            playerClone.transform.SetParent(this.transform);
            playerClone.Initialize();

            Transform randomSpawnPosition = GetRandomSpawPoint();
            playerClone.transform.position = randomSpawnPosition.position;

            DispatchOnPlayerSpawnEvent(playerClone);
        }

        private void DispatchOnPlayerSpawnEvent(PlayerUnit targetPlayer)
        {
            if (OnPlayerSpawnEvent != null)
                OnPlayerSpawnEvent(targetPlayer);
        }*/

        private Transform GetRandomSpawPoint()
        {
            Transform[] availableSpawnPosition = playerSpawPositionRoot.GetComponentsInChildren<Transform>();
            return availableSpawnPosition[Random.Range(0, availableSpawnPosition.Length)];
        }

        public void EnemyDied(HitInformation lastHitInformation)
        {
            DispatchEnemyDied(lastHitInformation);
        }

        private void DispatchEnemyDied(HitInformation hitInformation)
        {
            if (OnEnemyDiedEvent != null)
                OnEnemyDiedEvent(hitInformation);
        }
    }
}