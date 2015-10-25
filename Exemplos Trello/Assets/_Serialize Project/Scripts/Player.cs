using DataLoading;
using UnityEngine;

namespace SerializeProject
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerDataConfig playerdataConfig;
        [SerializeField]
        private PlayerData currentPlayerData;

        private DataLoader dataLoader;

#if UNITY_EDITOR
        [SerializeField] private bool hackExperienceUp;
        [SerializeField] private bool hackClearPlayerData;
#endif

        private void Start()
        {
            dataLoader = new DataLoader();

            LoadPlayerData();
            if(currentPlayerData == null)
            {
                CreateNewPlayerData();
                SavePlayerData();
            }
            LoadPlayerData();
            UpdatePlayerAttributes(true);
        }

        private void LoadPlayerData()
        {
            currentPlayerData = dataLoader.LoadDataObject<PlayerData>(new PlayerData());
        }

        private void SavePlayerData()
        {
            dataLoader.StoreDataObject<PlayerData>(currentPlayerData);
        }

      

        private void CreateNewPlayerData()
        {
            currentPlayerData = new PlayerData
            {
                PlayerName = "Player",
                CurrentExperience = 0,
                CurrentLevel = 1
            };
            Debug.Log("Created new player");
        }
        private void PlayerGainXP(int xpAmmount)
        {
            currentPlayerData.CurrentExperience += xpAmmount;
            SavePlayerData();
            UpdatePlayerAttributes(false);
        }

        private void UpdatePlayerAttributes(bool initialLoad)
        {
            LevelSpecification levelSpecification = GetLevelSpecification();

            if(levelSpecification == null)
                return; 

            if(levelSpecification.Level > currentPlayerData.CurrentLevel && initialLoad == false)
                Debug.Log("Player LEVELED UP");

            currentPlayerData.Health = levelSpecification.Health;
            currentPlayerData.MoveSpeed = levelSpecification.Movespeed;
            currentPlayerData.CurrentLevel = levelSpecification.Level;

        }

        private LevelSpecification GetLevelSpecification()
        {
            for (int i = 0; i < playerdataConfig.CharacterProgression.Length; i++)
            {
                LevelSpecification tempLevelSpecification = playerdataConfig.CharacterProgression[i];

                if (currentPlayerData.CurrentExperience >= tempLevelSpecification.MinimumExperience &&
                    currentPlayerData.CurrentExperience < tempLevelSpecification.MaximumExperience)
                    return tempLevelSpecification;
            }
            return null;
        }


#if UNITY_EDITOR
        private void Update()
        {
            if (hackExperienceUp)
            {
                PlayerGainXP(Random.Range(0, 100));
                hackExperienceUp = false;
            }


            if (hackClearPlayerData)
            {
                dataLoader.ClearData<PlayerData>(currentPlayerData);
                hackClearPlayerData = false;
            }
        }
#endif
    }
}

