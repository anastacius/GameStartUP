using UnityEngine;
using Gameplay.Unit;

namespace Gameplay
{
    [RequireComponent(typeof(TriggerVolume))]
    public abstract class BasePickable : MonoBehaviour
    {
        private TriggerVolume triggerVolume;

        private void Awake()
        {
            triggerVolume = GetComponent<TriggerVolume>();
        }
        private void OnEnable()
        {
            triggerVolume.OnTriggerEnterEvent += OnPlayerCollide;
        }


        private void OnDisable()
        {
            triggerVolume.OnTriggerEnterEvent -= OnPlayerCollide;
        }

        private void OnPlayerCollide(TriggerVolume triggerVolume, Collider collider)
        {
            PlayerUnit playerUnit = collider.GetComponent<PlayerUnit>();
            OnPicked(playerUnit);
            Destroy(gameObject);
        }

        protected abstract void OnPicked(PlayerUnit playerUnit);
    }
}
