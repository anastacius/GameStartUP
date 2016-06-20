using System;
using UnityEngine;

namespace SerializeProject
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField]
        private string playerName;
        [SerializeField]
        private int currentExperience;

        private LevelSpecification currentLevelSpecification;

        public LevelSpecification CurrentLevelSpecification
        {
            get { return currentLevelSpecification; }
            set { currentLevelSpecification = value; }
        }

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public int CurrentExperience
        {
            get { return currentExperience; }
            set { currentExperience = value; }
        }
    }
}
