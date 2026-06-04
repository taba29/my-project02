public static class TypeChart
{
    public static float GetMultiplier(string attackType, string targetType)
    {
        if (attackType == "Fire" && targetType == "Grass")
            return 2f;

        if (attackType == "Grass" && targetType == "Fire")
            return 0.5f;

        return 1f;
    }
}