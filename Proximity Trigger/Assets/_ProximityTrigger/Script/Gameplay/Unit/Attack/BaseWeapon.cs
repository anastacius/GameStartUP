using UnityEngine;


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


        private BaseUnit owner;
        private float lastShootTime = float.MinValue;
        public BaseUnit Owner
        {
            get { return owner; }
        }

        protected virtual void Awake()
        {
            SimplePool.Preload(bullet.gameObject, maxBulletsPreload);
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
            BaseBullet bulletClone = SimplePool.Spawn(bullet.gameObject).GetComponent<BaseBullet>();
            bulletClone.transform.position = bulletExitPoint.position;
            bulletClone.transform.forward = bulletExitPoint.forward;
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

        public int AmmoCount()
        {
            return currentWeaponDefinition.GetAmmo();
        }
        public void AddAmmo(int ammoAmount)
        {
            currentWeaponDefinition.AddAmmo(ammoAmount);
        }

        public void Initialize(BaseUnit baseUnit)
        {
            owner = baseUnit;
        }
    }
}
