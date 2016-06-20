using System;
using UnityEngine;

namespace SerializeProject
{
    [Serializable]
    public class LevelSpecification
    {
        public int Level;
        public int MinimumExperience;
        public int MaximumExperience;
        public int Health;
        public float Movespeed;
    }
}