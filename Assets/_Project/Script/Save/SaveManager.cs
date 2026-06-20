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

    // Moves
data.moves.Clear();

MoveData[] moves =
{
    PartyState.move1,
    PartyState.move2,
    PartyState.move3,
    PartyState.move4
};

foreach (MoveData move in moves)
{
    MoveSaveData moveSaveData = new MoveSaveData
    {
        moveName = move.moveName,
        power = move.power,
        currentPP = move.currentPP,
        maxPP = move.maxPP,
        accuracy = move.accuracy,
        category = move.category.ToString(),
        type = move.type,
        effect = move.effect
    };

    data.moves.Add(moveSaveData);
}
        
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

        // Moves
if (data.moves != null && data.moves.Count >= 4)
{
    MoveData[] moves =
    {
        PartyState.move1,
        PartyState.move2,
        PartyState.move3,
        PartyState.move4
    };

    for (int i = 0; i < 4; i++)
    {
        moves[i].moveName = data.moves[i].moveName;
        moves[i].power = data.moves[i].power;
        moves[i].currentPP = data.moves[i].currentPP;
        moves[i].maxPP = data.moves[i].maxPP;
        moves[i].accuracy = data.moves[i].accuracy;

        if (!string.IsNullOrEmpty(data.moves[i].category))
        {
            moves[i].category =
                System.Enum.Parse<MoveCategory>(data.moves[i].category);
        }

        moves[i].type = data.moves[i].type;
        moves[i].effect = data.moves[i].effect;
    }
}
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