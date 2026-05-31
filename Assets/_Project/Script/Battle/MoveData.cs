public enum MoveCategory
{
    Physical,
    Special,
    Status
}

[System.Serializable]
public class MoveData
{
    public string moveName;

    public int power;

    public int currentPP;
    public int maxPP;

    public int accuracy = 100;

    public MoveCategory category;

    public string type;
}
