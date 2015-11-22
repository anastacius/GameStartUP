using UnityEngine;

namespace Gameplay.Unit.Attack
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField]
        protected LayerMask hitLayerMask;
        protected WeaponDefinition currentWeaponDefinition;
        private float lastShootTime = float.MinValue;

        protected virtual void Awake()
        {
            currentWeaponDefinition = new WeaponDefinition(60, 0.1f, 5, 10);
        }

        public virtual bool HaveAmmo()
        {
            return  (currentWeaponDefinition.GetAmmo() > 0) ;
        }

        public virtual bool IsOnCooldown()
        {
            return !(lastShootTime < Time.time);
        }

        public virtual void Shoot()
        {
            Debug.LogFormat("Shoot: {0}", currentWeaponDefinition.GetAmmo());
            lastShootTime = Time.time + currentWeaponDefinition.GetCooldown();
            currentWeaponDefinition.SpentAmmo();
        }

    }
}
