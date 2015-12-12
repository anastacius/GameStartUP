using UnityEngine;
using System.Collections;
using Gameplay.Unit.Attack;

namespace Gameplay.Unit
{
    [RequireComponent(typeof(AimController))]
    public class PlayerUnit : BaseUnit
    {
        private AimController aimController;

        public AimController AimController
        {
            get { return aimController; }
        }


        protected override void Awake()
        {
            base.Awake();
            aimController = GetComponent<AimController>();
        }
    }
}