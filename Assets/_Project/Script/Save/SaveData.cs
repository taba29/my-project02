using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int level;
    public int exp;
    public int nextLevelExp;
    public int maxHP;
    public int currentHP;

    public bool missionFirstBattle;
    public bool missionDefeatSlime;
    public bool missionLevel2;

    public bool achievementFirstVictory;
    public bool achievementReachLevel2;
    public bool achievementUseFireMove;

    public string sceneName;

public float playerX;
public float playerY;
public float playerZ;

public int move1PP;
public int move2PP;
public int move3PP;
public int move4PP;

public List<MoveSaveData> moves = new List<MoveSaveData>();

public List<ItemSaveData> items = new();

public List<string> openedChestIds = new List<string>();

public List<string> defeatedEnemyIds = new List<string>();
}
