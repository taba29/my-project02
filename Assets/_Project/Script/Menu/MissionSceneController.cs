using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text missionText;

    private void Start()
    {
missionText.text =
    "任務\n\n" +

    (MissionState.firstBattle ? "☑ " : "☐ ")
    + "はじめて戦闘する\n" +

    (MissionState.defeatSlime ? "☑ " : "☐ ")
    + "スライムを1体倒す\n" +

    (MissionState.level2 ? "☑ " : "☐ ")
    + "レベル2になる";
    }

    public void BackToMap()
    {
        SceneManager.LoadScene("Map01");
    }
}