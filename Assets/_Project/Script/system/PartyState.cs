public static class PartyState
{
    public static string monsterName = "スライム";
    

    public static int maxHP = 30;
    public static int currentHP = 18;

    

    public static int level = 1;
public static int exp = 0;
public static int nextLevelExp = 30;


public static MoveData move1 = new MoveData
{
    moveName = "たいあたり",
    power = 10,
    currentPP = 35,
    maxPP = 35,
    accuracy = 30,
    category = MoveCategory.Physical,
    type = "Normal",
    effect = ""
};

public static MoveData move2 = new MoveData
{
    moveName = "なきごえ",
    power = 0,
    currentPP = 40,
    maxPP = 40,
    accuracy = 100,
    category = MoveCategory.Status,
    type = "Normal",
    effect = "LowerAttack"
};

public static MoveData move3 = new MoveData
{
    moveName = "ひっかく",
    power = 8,
    currentPP = 35,
    maxPP = 35,
    accuracy = 100,
    category = MoveCategory.Physical,
    type = "Normal",
    effect = ""
};

public static MoveData move4 = new MoveData
{
    moveName = "まもる",
    power = 0,
    currentPP = 10,
    maxPP = 10,
    accuracy = 100,
    category = MoveCategory.Status,
    type = "Normal",
    effect = ""
};
}