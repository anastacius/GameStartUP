using System;
using JSON;
using UnityEngine;

namespace SerializeProject
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] [JSONItem("playerName", typeof (string))]
        private string playerName;
        [SerializeField] [JSONItem("playerExperience", typeof (int))]
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
