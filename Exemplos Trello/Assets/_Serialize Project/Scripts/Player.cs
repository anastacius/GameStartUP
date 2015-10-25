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

        //The data loader, used to read / write json files
        private DataLoader dataLoader;

#if UNITY_EDITOR
        [SerializeField] private bool hackExperienceUp;
        [SerializeField] private bool hackClearPlayerData;
#endif

        private void Start()
        {
            //Creating a new instance of the data loader
            dataLoader = new DataLoader();

            LoadPlayerData();
            if(currentPlayerData == null)
            {
                CreateNewPlayerData();
                SavePlayerData();
            }
            LoadPlayerData();
            UpdatePlayerAttributes();
        }

        private void LoadPlayerData()
        {
            currentPlayerData = dataLoader.LoadDataObject(currentPlayerData);
        }

        private void SavePlayerData()
        {
            dataLoader.StoreDataObject(currentPlayerData);
        }

      

        private void CreateNewPlayerData()
        {
            currentPlayerData = new PlayerData
            {
                PlayerName = "Player",
                CurrentExperience = 0,
                CurrentLevelSpecification = playerdataConfig.CharacterProgression[0]
            };
            Debug.Log("Created new player");
        }
        private void PlayerGainXP(int xpAmmount)
        {
            currentPlayerData.CurrentExperience += xpAmmount;
            SavePlayerData();
            UpdatePlayerAttributes();
        }

        private void UpdatePlayerAttributes()
        {
            LevelSpecification levelByXP = GetLevelSpecification();

            if(currentPlayerData.CurrentLevelSpecification != null && levelByXP.Level > currentPlayerData.CurrentLevelSpecification.Level)
                Debug.Log("Player LEVELED UP");

            currentPlayerData.CurrentLevelSpecification = levelByXP;

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
            return playerdataConfig.CharacterProgression[playerdataConfig.CharacterProgression.Length - 1];
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
                dataLoader.ClearData(currentPlayerData);
                CreateNewPlayerData();
                hackClearPlayerData = false;
            }
        }
#endif
    }
}

