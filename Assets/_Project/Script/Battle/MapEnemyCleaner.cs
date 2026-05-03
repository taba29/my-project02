using UnityEngine;

public class MapEnemyCleaner : MonoBehaviour
{
    private void Start()
    {
        RestorePlayerPosition();
        RemoveDefeatedEnemy();
    }

    private void RestorePlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // 負けた時は最初の位置へ戻す
        if (BattleState.playerDefeated)
        {
            if (BattleState.hasInitialPlayerPosition)
            {
                player.transform.position = BattleState.initialPlayerPosition;
            }

            BattleState.playerDefeated = false;
            BattleState.hasPlayerReturnPosition = false;
            return;
        }

        // 勝った時や通常復帰時は戦闘前の位置へ戻す
        if (BattleState.hasPlayerReturnPosition)
        {
            player.transform.position = BattleState.playerReturnPosition;
            BattleState.hasPlayerReturnPosition = false;
        }
    }

    private void RemoveDefeatedEnemy()
{
    BattleEnemy[] enemies = FindObjectsOfType<BattleEnemy>();

    foreach (BattleEnemy enemy in enemies)
    {
        if (DefeatedEnemyManager.IsDefeated(enemy.uniqueEnemyId))
        {
            Destroy(enemy.gameObject);
        }
    }

    BattleState.playerWon = false;
    BattleState.currentEnemyId = "";
}
}