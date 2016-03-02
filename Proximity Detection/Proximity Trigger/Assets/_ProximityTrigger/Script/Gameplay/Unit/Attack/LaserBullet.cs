using UnityEngine;
using System.Collections;

namespace Gameplay.Unit.Attack
{
    public class LaserBullet : BaseBullet
    {
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public override void Initialize(BaseWeapon targetBaseWeapon)
        {
            base.Initialize(targetBaseWeapon);

            Ray shootRay = new Ray(transform.position, transform.forward);
            Vector3 finalShootPosition = transform.position + transform.forward * weaponDefinition.GetRange();
            RaycastHit hit;

            if (Physics.Raycast(shootRay, out hit, weaponDefinition.GetRange(), layerMask))
            {
                finalShootPosition = hit.point;

                IHitByBullet[] affectedObjects = hit.transform.GetComponents<IHitByBullet>();
                base.ApplyEffect(affectedObjects, hit.point);
            }

            finalShootPosition = new Vector3(finalShootPosition.x, transform.position.y, finalShootPosition.z);

            lineRenderer.SetPosition(0, targetBaseWeapon.GetExitPoint().position);
            lineRenderer.SetPosition(1, finalShootPosition);


            this.DestroyBullet(0.05f);
        }
    }
}