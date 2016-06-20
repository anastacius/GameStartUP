using UnityEngine;
using System.Collections;
using UnityEditor;

namespace SerializeProject
{

    [SerializeField]
    public class PlayerDataConfig : ScriptableObject
    {
        [MenuItem("Assets/Create/PlayerDataConfig")]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<PlayerDataConfig>();
        }


        [SerializeField] private LevelSpecification[] characterProgression;
        

        public LevelSpecification[] CharacterProgression {get { return characterProgression; } }

    }
}