using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartySceneController : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text hpText;

    void Start()
    {
        nameText.text = PartyState.monsterName;

        levelText.text = "Lv " + PartyState.level;

        hpText.text =
            "HP " +
            PartyState.currentHP +
            " / " +
            PartyState.maxHP;
    }

    public void BackToMap()
{
    SceneManager.LoadScene("Map01");
}
}