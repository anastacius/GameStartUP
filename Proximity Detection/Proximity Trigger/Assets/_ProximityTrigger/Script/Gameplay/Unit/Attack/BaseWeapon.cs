using UnityEngine;
using UnityPooler;

namespace Gameplay.Unit.Attack
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField]
        protected LayerMask hitLayerMask;
        [SerializeField]
        protected BaseBullet bullet;
        [SerializeField]
        protected int maxBulletsPreload = 10;
        [SerializeField]
        protected WeaponDefinition currentWeaponDefinition = new WeaponDefinition(60, 0.1f, 30, 5);
        [SerializeField]
        protected Transform bulletExitPoint;


        private float lastShootTime = float.MinValue;

        protected virtual void Awake()
        {
            bullet.gameObject.PopulatePool(maxBulletsPreload);
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
            lastShootTime = Time.time + currentWeaponDefinition.GetCooldown();
            currentWeaponDefinition.SpentAmmo();
            BaseBullet bulletClone = bullet.gameObject.Get().GetComponent<BaseBullet>();
            bulletClone.transform.SetParent(transform);
            bulletClone.transform.localPosition = Vector3.zero;
            bulletClone.transform.forward = transform.forward;
            bulletClone.Initialize(this);
        }

        public WeaponDefinition GetWeaponDefinition()
        {
            return currentWeaponDefinition;
        }

        public LayerMask GetWeaponLayerMask()
        {
            return hitLayerMask;
        }

        public Transform GetExitPoint()
        {
            return bulletExitPoint;
        }

        public void AddAmmo(int ammoAmount)
        {
            currentWeaponDefinition.AddAmmo(ammoAmount);
        }
    }
}
