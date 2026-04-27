using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    private bool isTriggered = false;
    private BattleEnemy battleEnemy;

    private void Awake()
    {
        battleEnemy = GetComponent<BattleEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered) return;

        if (other.CompareTag("Player"))
        {
            isTriggered = true;

            if (battleEnemy != null && battleEnemy.enemyData != null)
            {
                BattleState.currentEnemyId = battleEnemy.uniqueEnemyId;
                BattleState.currentEnemyName = battleEnemy.enemyData.enemyName;
                BattleState.currentEnemyHP = battleEnemy.enemyData.maxHP;
                BattleState.currentEnemyAttackPower = battleEnemy.enemyData.attackPower;
                BattleState.currentEnemySprite = battleEnemy.enemyData.enemySprite;
                BattleState.currentBattleBackgroundSprite = battleEnemy.enemyData.battleBackgroundSprite;
            }
            else
            {
                Debug.LogWarning("BattleEnemy または EnemyData が設定されていません。");
            }

            BattleState.playerReturnPosition = other.transform.position;
            BattleState.hasPlayerReturnPosition = true;

            BattleState.playerWon = false;
            BattleState.playerDefeated = false;

            SceneManager.LoadScene("BattleScene");
        }
    }
}