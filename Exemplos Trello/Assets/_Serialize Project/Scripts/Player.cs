using JSON;
using UnityEngine;

namespace SerializeProject
{
    public class Player : MonoBehaviour
    {
        private const string PlayerdataJson = "playerData";

        [SerializeField]
        private PlayerDataConfig playerdataConfig;
        private PlayerData currentPlayerData;

#if UNITY_EDITOR
        [SerializeField] private bool hackExperienceUp;
        [SerializeField] private bool hackClearPlayerData;
#endif

        private void Start()
        {
            if (!ExistPlayerData())
            {
                CreateNewPlayerData();
                SavePlayerData();
            }
            LoadPlayerData();
            UpdatePlayerAttributes(true);
        }

        private bool ExistPlayerData()
        {
            string json = PlayerPrefs.GetString(PlayerdataJson, string.Empty);
            if (string.IsNullOrEmpty(json))
                return false;

            return true;
        }

        private void LoadPlayerData()
        {
            string json = PlayerPrefs.GetString(PlayerdataJson, string.Empty);
            currentPlayerData = (PlayerData) JSONSerialize.Deserialize(typeof (PlayerData), json);
        }

        private void SavePlayerData()
        {
            if (currentPlayerData == null)
                return;

            PlayerPrefs.SetString(PlayerdataJson, JSONSerialize.Serialize(currentPlayerData));
        }

        private void ClearPlayerData()
        {
            string json = PlayerPrefs.GetString(PlayerdataJson, string.Empty);
            if (!string.IsNullOrEmpty(json))
            {
                PlayerPrefs.SetString(PlayerdataJson, string.Empty);
            }
        }

        private void CreateNewPlayerData()
        {
            currentPlayerData = new PlayerData
            {
                PlayerName = "Player",
                CurrentExperience = 0,
                CurrentLevel = 1
            };
            Debug.Log(currentPlayerData.PlayerName);
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
                ClearPlayerData();
                hackClearPlayerData = false;
            }
        }
#endif
    }
}

