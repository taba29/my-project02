using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string SavePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "save.json");
        }
    }

    public static string LoadedSceneName = "Map01";

    public static void Save()
    {
        SaveData data = new SaveData();

        data.items.Clear();

foreach (var item in InventoryManager.Instance.GetAllItems())
{
    ItemSaveData itemSaveData = new ItemSaveData
    {
        itemName = item.Key,
        count = item.Value
    };

    data.items.Add(itemSaveData);
}

        // Party
        data.level = PartyState.level;
        data.exp = PartyState.exp;
        data.nextLevelExp = PartyState.nextLevelExp;
        data.maxHP = PartyState.maxHP;
        data.currentHP = PartyState.currentHP;

        // Mission
        data.missionFirstBattle = MissionState.firstBattle;
        data.missionDefeatSlime = MissionState.defeatSlime;
        data.missionLevel2 = MissionState.level2;

        // Achievement
        data.achievementFirstVictory = AchievementState.firstVictory;
        data.achievementReachLevel2 = AchievementState.reachLevel2;
        data.achievementUseFireMove = AchievementState.useFireMove;

        
        
        data.sceneName = "Map01";

if (MapReturnState.hasReturnPosition)
{
    data.playerX = MapReturnState.returnPosition.x;
    data.playerY = MapReturnState.returnPosition.y;
    data.playerZ = MapReturnState.returnPosition.z;
}

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(SavePath, json);

        Debug.Log("セーブしました");
        Debug.Log(SavePath);
    }

    public static bool Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("セーブデータがありません");
            return false;
        }

        string json = File.ReadAllText(SavePath);

        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // Party
        PartyState.level = data.level;
        PartyState.exp = data.exp;
        PartyState.nextLevelExp = data.nextLevelExp;
        PartyState.maxHP = data.maxHP;
        PartyState.currentHP = data.currentHP;

        // Mission
        MissionState.firstBattle = data.missionFirstBattle;
        MissionState.defeatSlime = data.missionDefeatSlime;
        MissionState.level2 = data.missionLevel2;

        // Achievement
        AchievementState.firstVictory = data.achievementFirstVictory;
        AchievementState.reachLevel2 = data.achievementReachLevel2;
        AchievementState.useFireMove = data.achievementUseFireMove;


        // Inventory
InventoryManager.Instance.ClearItems();
// Inventory
if (InventoryManager.Instance != null)
{
    InventoryManager.Instance.ClearItems();

    foreach (ItemSaveData item in data.items)
    {
        InventoryManager.Instance.AddItem(item.itemName, item.count);
    }
}

        BattleState.playerReturnPosition =
    new Vector3(data.playerX, data.playerY, data.playerZ);

MapReturnState.returnPosition =
    new Vector3(data.playerX, data.playerY, data.playerZ);

MapReturnState.hasReturnPosition = true;

LoadedSceneName = data.sceneName;

if (string.IsNullOrEmpty(LoadedSceneName))
{
    LoadedSceneName = "Map01";
}

        Debug.Log("ロードしました");

        return true;
    }

    public static bool HasSave()
{
    return File.Exists(SavePath);
}



public static void Delete()
{
    if (File.Exists(SavePath))
    {
        File.Delete(SavePath);
        Debug.Log("セーブデータを削除しました");
    }
}



}