public static class TypeChart
{
    public static float GetMultiplier(string attackType, string targetType)
    {
        // Fire
        if (attackType == "Fire" && targetType == "Grass")
            return 2f;

        if (attackType == "Fire" && targetType == "Water")
            return 0.5f;

        // Water
        if (attackType == "Water" && targetType == "Fire")
            return 2f;

        if (attackType == "Water" && targetType == "Grass")
            return 0.5f;

        // Grass
        if (attackType == "Grass" && targetType == "Water")
            return 2f;

        if (attackType == "Grass" && targetType == "Fire")
            return 0.5f;

        return 1f;
    }
}