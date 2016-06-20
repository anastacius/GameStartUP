using UnityEngine;
using Gameplay.Unit.Attack;

namespace Gameplay.Score
{
    public class ScoreController : MonoBehaviour
    {
        public delegate void ScoreDelegate(int currentScore);

        public event ScoreDelegate OnScoreChangedEvent;

        private const int ENEMY_SCORE = 100;

        private int score = 0;


        private void Awake()
        {
            GameplayController.Instance.OnEnemyDiedEvent += OnEnemyDied;
            GameplayController.Instance.GameStartedEvent += OnGameStarted;
        }

        private void OnDestroy()
        {
            GameplayController.Instance.OnEnemyDiedEvent -= OnEnemyDied;
            GameplayController.Instance.GameStartedEvent -= OnGameStarted;
        }
        private void OnGameStarted()
        {
            ChangeScore(0);
        }

        private void OnEnemyDied(HitInformation hitInformation)
        {
            AddScore(ENEMY_SCORE);
        }

        private void AddScore(int enemyScore)
        {
            ChangeScore(score + enemyScore);
        }

        private void ChangeScore(int targetScore)
        {
            score = targetScore;
            DispatchScoreChangedEvent(score);
        }

        private void DispatchScoreChangedEvent(int targetScore)
        {
            if (OnScoreChangedEvent != null)
                OnScoreChangedEvent(targetScore);
        }
    }
}