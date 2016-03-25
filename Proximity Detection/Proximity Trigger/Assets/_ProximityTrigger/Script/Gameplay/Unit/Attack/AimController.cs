using UnityEngine;
using UnityEngine.Networking;

namespace Gameplay.Unit.Attack
{
    public class AimController : NetworkBehaviour
    {
        private BaseWeapon currentWeapon;
        private BaseUnit baseUnit;

        public BaseWeapon CurrentWeapon
        {
            get { return currentWeapon; }
        }

        private void Awake()
        {
            baseUnit = GetComponent<BaseUnit>();
            currentWeapon = GetComponentInChildren<BaseWeapon>();
            currentWeapon.Initialize(baseUnit);
        }

        private void Update()
        {
            if(!isLocalPlayer)
                return;


            if (Input.GetMouseButton(0))
            {
                if(!currentWeapon.HaveAmmo())
                    return;

                if(currentWeapon.IsOnCooldown())
                    return;

                currentWeapon.Shoot();
            }
        }

    }


}
