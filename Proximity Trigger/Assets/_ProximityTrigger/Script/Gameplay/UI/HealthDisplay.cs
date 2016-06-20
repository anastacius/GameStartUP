using System;
using DG.Tweening;
using Gameplay.Attribute;
using Gameplay.Unit;
using UnityEngine;
using UnityEngine.UI;
using Attribute = Gameplay.Attribute.Attribute;

namespace UI
{
    public class HealthDisplay : MonoBehaviour, IUIFromPlayer
    {
        [SerializeField]
        private Image healthBar;
        [SerializeField]
        private Color[] healthColors;


        private PlayerUnit currentPlayer;
        private Attribute healthAttribute;

        public void Initialize(PlayerUnit player)
        {
            if(healthAttribute != null)
                healthAttribute.OnAttributeChange -= OnAttributeChange;

            currentPlayer = player;
            healthAttribute = currentPlayer.AttributePool.GetAttribute(AttributeType.Health);

            healthAttribute.OnAttributeChange += OnAttributeChange;


            OnAttributeChange(healthAttribute.MaxValue, healthAttribute.CurrentValue);
        }

        private void OnDestroy()
        {
            if(healthAttribute != null)
                healthAttribute.OnAttributeChange -= OnAttributeChange;
        }

        private void OnAttributeChange(float prevValue, float currentValue)
        {
            healthBar.DOFillAmount(currentValue/healthAttribute.MaxValue, 0.4f)
                .SetEase(Ease.OutBack);

            Color targetColor = GetTargetColor(healthBar.fillAmount);

            healthBar.DOColor(targetColor, 0.5f);
        }

        private Color GetTargetColor(float fillAmount)
        {
            if (fillAmount > 0 && fillAmount <= 0.33f)
            {
                return healthColors[0];
            }
            else if (fillAmount > 0.33 && fillAmount <= 0.66f)
            {
                return healthColors[1];
            }
            return healthColors[2];
        }
    }
}