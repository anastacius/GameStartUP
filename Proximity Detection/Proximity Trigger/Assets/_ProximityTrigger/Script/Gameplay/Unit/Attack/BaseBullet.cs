using UnityEngine;
using System.Collections;
using UnityPooler;

namespace Gameplay.Unit.Attack
{
    public class BaseBullet : MonoBehaviour
    {
        protected BaseWeapon baseWeapon;
        protected WeaponDefinition weaponDefinition;
        protected LayerMask layerMask;

        public virtual void Initialize(BaseWeapon targetBaseWeapon)
        {
            baseWeapon = targetBaseWeapon;
            weaponDefinition = baseWeapon.GetWeaponDefinition();
            layerMask = baseWeapon.GetWeaponLayerMask();
        }

        protected virtual void DestroyBullet(float afterSeconds)
        {
            this.StartCoroutine(DestroyBulletAfterSecondsCoroutine(afterSeconds));
        }
        private IEnumerator DestroyBulletAfterSecondsCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            gameObject.Release();
        }


        protected void ApplyEffect(IHitByBullet[] affectedObjects)
        {
            for (int i = 0; i < affectedObjects.Length; i++)
            {
                IHitByBullet affectedObject = affectedObjects[i];
                affectedObject.Hit(baseWeapon);
            }
        }
    }
}