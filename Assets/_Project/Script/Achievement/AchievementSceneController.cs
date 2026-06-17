using TMPro;
using UnityEngine;

public class AchievementSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text achievementText;


    private void Start()
{
    UpdateAchievementText();
}

    private void UpdateAchievementText()
    {
        string text = "";

        text += GetCheck(AchievementState.firstVictory) + " 初めて勝利する\n";
        text += GetCheck(AchievementState.reachLevel2) + " レベル2になる\n";
        text += GetCheck(AchievementState.useFireMove) + " ひのこを使う\n";

        achievementText.text = text;
    }

    private string GetCheck(bool done)
    {
        return done ? "[OK]" : "[  ]";
    }

    
}