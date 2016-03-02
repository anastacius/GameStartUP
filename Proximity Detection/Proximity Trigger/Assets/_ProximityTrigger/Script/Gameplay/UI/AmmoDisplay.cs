using UnityEngine;
using System.Collections;
using Gameplay.Unit;
using Gameplay.Unit.Attack;
using UnityEngine.UI;

namespace UI
{
    public class AmmoDisplay : MonoBehaviour, IUIFromPlayer
    {
        [SerializeField]
        private Text ammoLabel;

        private BaseWeapon baseWeapon;
        private Coroutine updateRoutine;

        public void Initialize(PlayerUnit player)
        {
            baseWeapon = player.AimController.CurrentWeapon;

            if(updateRoutine!=null)
                StopCoroutine(updateRoutine);

            updateRoutine = StartCoroutine(UpdateCoroutine());
        }

        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                ammoLabel.text = baseWeapon.AmmoCount().ToString("00");
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
