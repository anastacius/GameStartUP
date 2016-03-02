using UnityEngine;
using Gameplay;
using Gameplay.Unit;

namespace UI
{
    public class GameplayScreen : MonoBehaviour
    {
        private GameplayController gameplayInstance;
        private IUIFromPlayer[] playerDependend;

        private void Awake()
        {
            gameplayInstance = GameplayController.Instance;

            playerDependend = GetComponentsInChildren<IUIFromPlayer>();

            gameplayInstance.OnPlayerSpawnEvent += OnPlayerSpawn;
        }

        private void OnDestroy()
        {
            gameplayInstance.OnPlayerSpawnEvent += OnPlayerSpawn;
        }


        private void OnPlayerSpawn(PlayerUnit player)
        {
            for (int i = 0; i < playerDependend.Length; i++)
            {
                IUIFromPlayer uiFromPlayer = playerDependend[i];
                uiFromPlayer.Initialize(player);
            }
        }
    }
}

