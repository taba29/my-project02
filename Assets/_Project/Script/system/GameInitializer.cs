using UnityEngine;

public static class GameInitializer
{
    public static void NewGame()
    {
        // Party
        PartyState.level = 1;
        PartyState.exp = 0;
        PartyState.nextLevelExp = 30;
        PartyState.maxHP = 30;
        PartyState.currentHP = 30;

        // Mission
        MissionState.firstBattle = false;
        MissionState.defeatSlime = false;
        MissionState.level2 = false;

        // Achievement
        AchievementState.firstVictory = false;
        AchievementState.reachLevel2 = false;
        AchievementState.useFireMove = false;

        // Map position
        MapReturnState.hasReturnPosition = false;
        MapReturnState.returnPosition = Vector3.zero;

        Debug.Log("ニューゲーム初期化しました");
    }
}