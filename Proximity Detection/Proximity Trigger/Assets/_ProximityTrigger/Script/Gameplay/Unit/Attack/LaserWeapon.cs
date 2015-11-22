using UnityEngine;

namespace Gameplay.Unit.Attack
{
    public class LaserWeapon : BaseWeapon
    {
        private LineRenderer lineRenderer;
        protected override void Awake()
        {
            base.Awake();
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }


        public override void Shoot()
        {
            base.Shoot();

            Ray shootRay = new Ray(transform.position, transform.forward);
            Vector3 finalShootPosition = transform.position + transform.forward*currentWeaponDefinition.GetRange();
            RaycastHit hit;

            if (Physics.Raycast(shootRay, out hit, currentWeaponDefinition.GetRange(), hitLayerMask.value))
                finalShootPosition = hit.point;

            finalShootPosition = new Vector3(finalShootPosition.x, transform.position.y, finalShootPosition.z);

            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, finalShootPosition);
        }
    }
}

