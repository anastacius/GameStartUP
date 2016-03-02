using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField]
        private Text scoreText;

        private int score;

        private void Awake()
        {
            GameplayController.Instance.ScoreController.OnScoreChangedEvent += OnScoreChanged;
        }

        private void OnDestroy()
        {
            GameplayController.Instance.ScoreController.OnScoreChangedEvent -= OnScoreChanged;
        }

        private void OnScoreChanged(int currentScore)
        {
            scoreText.transform.localScale = new Vector3(1, 1.5f, 1);
            scoreText.transform.DOScaleY(1, 0.5f).SetEase(Ease.OutBack);
            DOTween.To(() => score, x => score = x, currentScore, 0.5f).OnUpdate(() =>
            {
                scoreText.text = score.ToString();
            });
        }
    }
}
