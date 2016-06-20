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

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            GameplayController.Instance.DispatchOnPlayerSpawnEvent(this);
        }

        protected override void Awake()
        {
            base.Awake();
            aimController = GetComponent<AimController>();
        }
        
    }
}