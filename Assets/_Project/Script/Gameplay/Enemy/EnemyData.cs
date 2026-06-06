using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyId = "enemy_001";
    public string enemyName = "Slime";
    public int maxHP = 20;
    public int attackPower = 4;
    
    public string type = "Normal";
    
    public Sprite enemySprite;
    public Sprite battleBackgroundSprite;
}