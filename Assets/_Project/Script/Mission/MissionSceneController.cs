using TMPro;
using UnityEngine;

public class MissionSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text missionText;

    private void Start()
    {
        missionText.text =
            "任務\n\n" +

            (MissionState.firstBattle ? "good " : "☐ ")
            + "はじめて戦闘する\n" +

            (MissionState.defeatSlime ? "good " : "☐ ")
            + "スライムを1体倒す\n" +

            (MissionState.level2 ? "good " : "☐ ")
            + "レベル2になる";
    }
}