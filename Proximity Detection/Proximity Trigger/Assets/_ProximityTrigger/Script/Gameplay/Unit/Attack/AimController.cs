using UnityEngine;
using System.Collections;

namespace Gameplay.Unit.Attack
{
    public class AimController : MonoBehaviour
    {
        private BaseWeapon currentBaseWeapon;

        private void Awake()
        {
            currentBaseWeapon = GetComponentInChildren<BaseWeapon>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if(!currentBaseWeapon.HaveAmmo())
                    return;

                if(currentBaseWeapon.IsOnCooldown())
                    return;

                currentBaseWeapon.Shoot();
            }
        }

    }


}
