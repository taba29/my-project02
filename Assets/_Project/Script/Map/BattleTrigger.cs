using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;



public class BattleTrigger : MonoBehaviour
{

    [SerializeField] private Image battleFlash;

    private bool isTriggered = false;
    private BattleEnemy battleEnemy;
    private bool isStartingBattle = false;

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
                BattleState.currentEnemyType = battleEnemy.enemyData.type;
            }
            else
            {
                Debug.LogWarning("BattleEnemy または EnemyData が設定されていません。");
            }

            BattleState.playerReturnPosition = other.transform.position;
            BattleState.hasPlayerReturnPosition = true;

            BattleState.playerWon = false;
            BattleState.playerDefeated = false;

            StartCoroutine(StartBattle());
             }

    }
  private IEnumerator StartBattle()
{
    isStartingBattle = true;

    Color c = battleFlash.color;

    c.a = 1f;
    battleFlash.color = c;

    yield return new WaitForSeconds(0.08f);

    float time = 0f;

    while (time < 0.2f)
    {
        time += Time.deltaTime;

        c.a = Mathf.Lerp(1f, 0f, time / 0.2f);
        battleFlash.color = c;

        yield return null;
    }

    SceneManager.LoadScene("BattleScene");
}
}