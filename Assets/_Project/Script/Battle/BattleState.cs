using UnityEngine;

public static class BattleState
{
    public static string currentEnemyId = "";
    public static string currentEnemyName = "";
    public static int currentEnemyHP = 20;
    public static int currentEnemyAttackPower = 4;
    public static Sprite currentEnemySprite = null;
    public static Sprite currentBattleBackgroundSprite = null;

    public static bool playerWon = false;
    public static bool playerDefeated = false;

    public static Vector3 playerReturnPosition = Vector3.zero;
    public static bool hasPlayerReturnPosition = false;

    public static Vector3 initialPlayerPosition = Vector3.zero;
    public static bool hasInitialPlayerPosition = false;

    public static string currentEnemyType;
}