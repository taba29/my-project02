using System.Collections.Generic;

public static class DefeatedEnemyManager
{
    private static HashSet<string> defeatedEnemyIds = new HashSet<string>();

    public static void AddDefeatedEnemy(string enemyId)
    {
        if (string.IsNullOrEmpty(enemyId)) return;

        defeatedEnemyIds.Add(enemyId);
    }

    public static bool IsDefeated(string enemyId)
    {
        if (string.IsNullOrEmpty(enemyId)) return false;

        return defeatedEnemyIds.Contains(enemyId);
    }

    public static void Clear()
    {
        defeatedEnemyIds.Clear();
    }

    public static IEnumerable<string> GetAllDefeatedEnemies()
{
    return defeatedEnemyIds;
}
}