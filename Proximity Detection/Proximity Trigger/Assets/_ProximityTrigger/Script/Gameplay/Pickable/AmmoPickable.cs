using UnityEngine;
using System.Collections;
using Gameplay.Unit;

namespace Gameplay
{
    public class AmmoPickable : BasePickable
    {
        [SerializeField]
        private int ammoAmount = 30;
        protected override void OnPicked(PlayerUnit playerUnit)
        {
            playerUnit.AimController.CurrentWeapon.AddAmmo(ammoAmount);
        }
    }
}