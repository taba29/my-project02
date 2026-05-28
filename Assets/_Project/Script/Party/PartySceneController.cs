using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartySceneController : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text hpText;
    public TMP_Text expText;

    public TMP_Text move1Text;
    public TMP_Text move2Text;
    
    [SerializeField] private RectTransform hpBarFill;
[SerializeField] private float maxBarWidth = 490f;
    

    void Start()
    {
        nameText.text = PartyState.monsterName;

        levelText.text = "Lv " + PartyState.level;

        hpText.text =
            "HP " +
            PartyState.currentHP +
            " / " +
            PartyState.maxHP;

        expText.text =
    "EXP " +
    PartyState.exp +
    " / " +
    PartyState.nextLevelExp;

    move1Text.text =
    PartyState.move1Name + " " +
    PartyState.move1PP + " / " +
    PartyState.move1MaxPP;

    move2Text.text =
    PartyState.move2Name + " " +
    PartyState.move2PP + " / " +
    PartyState.move2MaxPP;

            UpdateHPBar();
    }

    private void UpdateHPBar()
{
    float ratio =
        (float)PartyState.currentHP / PartyState.maxHP;

    Vector2 size = hpBarFill.sizeDelta;
    size.x = maxBarWidth * ratio;

    hpBarFill.sizeDelta = size;
}

    public void BackToMap()
{
    SceneManager.LoadScene("Map01");
}
}
