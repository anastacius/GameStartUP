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

        private int currentLevel;
        private int health;
        private float moveSpeed;


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
        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

      
    }
}
