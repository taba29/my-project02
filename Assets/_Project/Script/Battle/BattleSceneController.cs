using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BattleSceneController : MonoBehaviour
{
   [SerializeField] private RectTransform playerHPBarFill;
[SerializeField] private RectTransform enemyHPBarFill;
[SerializeField] private float maxPlayerHPBarWidth = 398f;
[SerializeField] private float maxEnemyHPBarWidth = 398f;
[SerializeField] private TMP_Text move2Text;
[SerializeField] private Button move2Button;



[SerializeField] private TMP_Text move1Text;
private int enemyMaxHP;
    [Header("Fade")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Background")]
    [SerializeField] private Image backgroundImage;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerHPText;
    [SerializeField] private TextMeshProUGUI enemyHPText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button move1Button;
    [SerializeField] private Button backButton;

    [Header("Enemy View")]
    [SerializeField] private Image enemyImage;
    [SerializeField] private float shakeDistance = 20f;
    [SerializeField] private float shakeTime = 0.2f;

    

    [Header("Enemy Status")]
    [SerializeField] private string enemyName = "Enemy";
    [SerializeField] private int enemyHP = 20;
    [SerializeField] private int enemyAttackPower = 4;

    private bool battleEnded = false;

    private void Start()
    {
        LoadEnemyDataFromState();

        UpdateHPText();
        resultText.text = "A wild " + enemyName + " appeared!";
        backButton.gameObject.SetActive(true);

        move1Text.text =
        PartyState.move1.moveName + "\n" +
        PartyState.move1.currentPP + " / " +
        PartyState.move1.maxPP;

        move2Text.text =
        PartyState.move2.moveName + "\n" +
        PartyState.move2.currentPP + " / " +
        PartyState.move2.maxPP;

        StartCoroutine(FadeIn());
    }

    private void LoadEnemyDataFromState()
    {
        enemyName = BattleState.currentEnemyName;
        enemyHP = BattleState.currentEnemyHP;
        enemyAttackPower = BattleState.currentEnemyAttackPower;
        enemyMaxHP = enemyHP;

        if (string.IsNullOrEmpty(enemyName))
        {
            enemyName = "Enemy";
        }

        if (enemyImage != null)
        {
            enemyImage.sprite = BattleState.currentEnemySprite;
            enemyImage.enabled = BattleState.currentEnemySprite != null;
        }

        if (backgroundImage != null)
        {
            backgroundImage.sprite = BattleState.currentBattleBackgroundSprite;
            backgroundImage.enabled = BattleState.currentBattleBackgroundSprite != null;
        }
    }

    public void OnAttackButton()
{
    OnMove1Button();
}

    private IEnumerator PlayerAttackSequence(MoveData move)
    {
        move1Button.interactable = false;
        move2Button.interactable = false;
        
        resultText.text = move.moveName + "！";
        yield return new WaitForSeconds(0.3f);
        

        if (Random.Range(0, 100) >= move.accuracy)
{
    resultText.text = move.moveName + " は外れた！";
    yield return new WaitForSeconds(0.7f);

    yield return StartCoroutine(EnemyAttackSequence());

    move1Button.interactable = true;
    move2Button.interactable = true;

    yield break;
}

        if (enemyImage != null)
        {
            yield return StartCoroutine(ShakeEnemy());
        }
if (move.category == MoveCategory.Status)
{
    ApplyMoveEffect(move);

    resultText.text = enemyName + " の攻撃が下がった！";

    yield return new WaitForSeconds(0.7f);
}
else
{
    enemyHP -= move.power;
    if (enemyHP < 0) enemyHP = 0;

    UpdateHPText();

    resultText.text = enemyName + " took " + move.power + " damage!";
    yield return new WaitForSeconds(0.7f);
}

        if (enemyHP <= 0)
        {
            battleEnded = true;
            BattleState.playerWon = true;
            BattleState.playerDefeated = false;

            DefeatedEnemyManager.AddDefeatedEnemy(BattleState.currentEnemyId);

            PartyState.exp += 10;

if (PartyState.exp >= PartyState.nextLevelExp)
{
    PartyState.level++;

    PartyState.exp = 0;

    PartyState.maxHP += 10;

    PartyState.currentHP = PartyState.maxHP;

    resultText.text =
        "Level Up! Lv." +
        PartyState.level;
}
else
{
    resultText.text =
        "Victory!\nEXP +10";
}

yield return new WaitForSeconds(2f);

SceneManager.LoadScene("Map01");
            yield break;
        }
        yield return StartCoroutine(EnemyAttackSequence());

move1Button.interactable = true;
move2Button.interactable = true;
    }

    public void BackToMap()
    {
        SceneManager.LoadScene("Map01");
    }

    private void UpdateHPText()
{
    playerHPText.text =
        PartyState.monsterName + " HP: " +
        PartyState.currentHP + " / " +
        PartyState.maxHP;

    enemyHPText.text = enemyName + " HP: " + enemyHP + " / " + enemyMaxHP;

    UpdateHPBar(playerHPBarFill, PartyState.currentHP, PartyState.maxHP, maxPlayerHPBarWidth);
    UpdateHPBar(enemyHPBarFill, enemyHP, enemyMaxHP, maxEnemyHPBarWidth);
}

private void UpdateHPBar(RectTransform fill, int hp, int maxHP, float maxWidth)
{
    if (fill == null) return;

    float ratio = (float)hp / maxHP;
    float targetWidth = maxWidth * ratio;

    StartCoroutine(AnimateHPBar(fill, targetWidth, 0.25f));
}
    private IEnumerator FadeIn()
    {
        float time = 0f;
        Color color = fadePanel.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(1f, 0f, t);
            fadePanel.color = color;
            yield return null;
        }

        color.a = 0f;
        fadePanel.color = color;
    }

    private IEnumerator ShakeEnemy()
    {
        RectTransform rt = enemyImage.rectTransform;
        Vector2 originalPos = rt.anchoredPosition;
        float halfTime = shakeTime * 0.5f;

        rt.anchoredPosition = originalPos + new Vector2(shakeDistance, 0f);
        yield return new WaitForSeconds(halfTime);

        rt.anchoredPosition = originalPos + new Vector2(-shakeDistance, 0f);
        yield return new WaitForSeconds(halfTime);

        rt.anchoredPosition = originalPos;
    }

    private IEnumerator AnimateHPBar(RectTransform fill, float targetWidth, float duration)
{
    float startWidth = fill.sizeDelta.x;
    float time = 0f;

    while (time < duration)
    {
        time += Time.unscaledDeltaTime;

        Vector2 size = fill.sizeDelta;
        size.x = Mathf.Lerp(startWidth, targetWidth, time / duration);
        fill.sizeDelta = size;

        yield return null;
    }

    Vector2 finalSize = fill.sizeDelta;
    finalSize.x = targetWidth;
    fill.sizeDelta = finalSize;
}

public void OnMove1Button()
{
    if (battleEnded) return;

    if (PartyState.move1.currentPP <= 0)
    {
        resultText.text = "PPがない！";
        return;
    }

    PartyState.move1.currentPP--;

UpdateUI();

StartCoroutine(PlayerAttackSequence(PartyState.move1));
}



void UpdateUI()
{
    enemyHPText.text = enemyName + " HP: " + enemyHP + " / " + enemyMaxHP;

    move1Text.text =
    PartyState.move1.moveName + "\n" +
    PartyState.move1.currentPP + " / " +
    PartyState.move1.maxPP;

    move2Text.text =
    PartyState.move2.moveName + "\n" +
    PartyState.move2.currentPP + " / " +
    PartyState.move2.maxPP;
}

public void OnMove2Button()
{
    if (battleEnded) return;

    if (PartyState.move2.currentPP <= 0)
    {
        resultText.text = "PPがない！";
        return;
    }

    move1Button.interactable = false;
    move2Button.interactable = false;

    PartyState.move2.currentPP--;

UpdateUI();

StartCoroutine(PlayerAttackSequence(PartyState.move2));
}



private IEnumerator EnemyAttackSequence()
{
    resultText.text = enemyName + " Attack!";
    yield return new WaitForSeconds(0.5f);

    PartyState.currentHP -= enemyAttackPower;

    if (PartyState.currentHP < 0)
        PartyState.currentHP = 0;

    UpdateHPText();

    resultText.text =
        "Player took " +
        enemyAttackPower +
        " damage!";

    yield return new WaitForSeconds(0.7f);

    if (PartyState.currentHP <= 0)
    {
        battleEnded = true;

        BattleState.playerWon = false;
        BattleState.playerDefeated = true;

        resultText.text = "Defeat...";

        yield return new WaitForSeconds(1.2f);

        SceneManager.LoadScene("Map01");
        yield break;
    }

    resultText.text = "Choose your action.";
}

private void ApplyMoveEffect(MoveData move)
{
    if (move.effect == "LowerAttack")
    {
        enemyAttackPower -= 1;

        if (enemyAttackPower < 1)
            enemyAttackPower = 1;
    }
}
}

