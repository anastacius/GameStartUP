using System;
using UnityEngine;

namespace Gameplay.Unit.Attack
{
    [Serializable]
    public class WeaponDefinition
    {
        [SerializeField]
        private int ammo;
        [SerializeField]
        private float cooldown;
        [SerializeField]
        private int damage;
        [SerializeField]
        private float range;

        public WeaponDefinition(int ammo, float cooldown, float range, int damage)
        {
            this.ammo = ammo;
            this.cooldown = cooldown;
            this.range = range;
            this.damage = damage;
        }

        public int GetAmmo()
        {
            return ammo;
        }

        public float GetCooldown()
        {
            return cooldown;
        }

        public void SpentAmmo()
        {
            ammo--;
        }

        public float GetRange()
        {
            return range;
        }

        public int GetDamage()
        {
            return damage;
        }

        public void AddAmmo(int ammoAmount)
        {
            ammo += ammoAmount;
        }
    }
}