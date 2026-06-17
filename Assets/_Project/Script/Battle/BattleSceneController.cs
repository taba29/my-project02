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
[SerializeField] private TMP_Text move3Text;
[SerializeField] private TMP_Text move4Text;
[SerializeField] private Button move3Button;
[SerializeField] private Button move4Button;
[SerializeField] private Image fireEffect;
[SerializeField] private Image playerImage;

[SerializeField] private Sprite fireSprite;
[SerializeField] private Sprite slashSprite;

[SerializeField] private AudioSource victoryBGM;
[SerializeField] private AudioSource battleBGM;

[SerializeField] private AudioSource seAudioSource;
[SerializeField] private AudioClip scratchSE;
[SerializeField] private AudioClip fireSE;

[SerializeField] private Image attackDarkPanel;
[SerializeField] private Image attackSpeedLine;

[SerializeField] private TMP_Text move1Text;

[SerializeField] private SpeedLineScroller[] speedLines;
[SerializeField]
private Image battleGradient;

[SerializeField] private Image hitExplosion;
[SerializeField] private Image shockwaveEffect;

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
    [SerializeField] private string enemyType = "Grass";

    private bool battleEnded = false;

    private void Start()
    {
        if (BGMManager.Instance != null)
{
    BGMManager.Instance.StopBGM();
}

        MissionState.firstBattle = true;

        fireEffect.gameObject.SetActive(false);
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

        move3Text.text =
    PartyState.move3.moveName + "\n" +
    PartyState.move3.currentPP + " / " +
    PartyState.move3.maxPP;

move4Text.text =
    PartyState.move4.moveName + "\n" +
    PartyState.move4.currentPP + " / " +
    PartyState.move4.maxPP;

        StartCoroutine(FadeIn());

        if (attackDarkPanel != null)
{
    Color c = attackDarkPanel.color;
    c.a = 0f;
    attackDarkPanel.color = c;
    attackDarkPanel.gameObject.SetActive(false);
}

if (attackSpeedLine != null)
{
    Color c = attackSpeedLine.color;
    c.a = 0f;
    attackSpeedLine.color = c;
    attackSpeedLine.gameObject.SetActive(false);
}

if (battleGradient != null)
{
    Color c = battleGradient.color;
    c.a = 0f;
    battleGradient.color = c;
    battleGradient.gameObject.SetActive(false);
}

if (hitExplosion != null)
{
    hitExplosion.gameObject.SetActive(false);
}

if (shockwaveEffect != null)
{
    shockwaveEffect.gameObject.SetActive(false);
}
    }

    private void LoadEnemyDataFromState()
    {
        enemyName = BattleState.currentEnemyName;
        enemyHP = BattleState.currentEnemyHP;
        enemyAttackPower = BattleState.currentEnemyAttackPower;
        enemyType = BattleState.currentEnemyType;
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
        move3Button.interactable = false;
        move4Button.interactable = false;
        
        resultText.text = move.moveName + "！";
        yield return new WaitForSeconds(0.3f);
        
        if (move.moveName == "ひのこ")
{   AchievementState.useFireMove = true;
    
    fireEffect.sprite = fireSprite;

   yield return StartCoroutine(PlayFireEffect());
yield return StartCoroutine(PlayHitExplosion());
yield return StartCoroutine(FlashEnemy());
}

if (move.moveName == "ひっかく")
{
    fireEffect.sprite = slashSprite;

    yield return StartCoroutine(
        PlayScratchAttack());

}

        if (Random.Range(0, 100) >= move.accuracy)
{
    resultText.text = move.moveName + " は外れた！";
    yield return new WaitForSeconds(0.7f);

    yield return StartCoroutine(EnemyAttackSequence());

    move1Button.interactable = true;
move2Button.interactable = true;
move3Button.interactable = true;
move4Button.interactable = true;
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
   

    float multiplier =
        TypeChart.GetMultiplier(move.type, enemyType);
        
   

    int damage =
        Mathf.RoundToInt(move.power * multiplier);

    enemyHP -= damage;

    if (enemyHP < 0)
        enemyHP = 0;

    UpdateHPText();

    if (multiplier > 1f)
    {
        resultText.text =
            enemyName + " took " + damage +
            " damage!\nこうかはばつぐんだ！";
    }
    else if (multiplier < 1f)
    {
        resultText.text =
            enemyName + " took " + damage +
            " damage!\nこうかはいまひとつのようだ";
    }
    else
    {
        resultText.text =
            enemyName + " took " + damage + " damage!";
    }

    yield return new WaitForSeconds(0.7f);
}

        if (enemyHP <= 0)
        {
            battleEnded = true;
            BattleState.playerWon = true;
            BattleState.playerDefeated = false;

            AchievementState.firstVictory = true;

            DefeatedEnemyManager.AddDefeatedEnemy(BattleState.currentEnemyId);

            if (enemyName == "スライム")
{
    MissionState.defeatSlime = true;
}

            PartyState.exp += 10;

if (PartyState.exp >= PartyState.nextLevelExp)
{
    PartyState.level++;

    if(PartyState.level >= 2)
    {MissionState.level2 = true;
    
    AchievementState.reachLevel2 = true;}

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

yield return StartCoroutine(
    FadeOutBGM(battleBGM, 1f));

victoryBGM.Play();

yield return new WaitForSeconds(2f);

yield return StartCoroutine(
    FadeOutBGM(victoryBGM, 1f));





SceneManager.LoadScene("Map01");
            yield break;
        }
        yield return StartCoroutine(EnemyAttackSequence());

move1Button.interactable = true;
move2Button.interactable = true;
move3Button.interactable = true;
move4Button.interactable = true;
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

    move3Text.text =
    PartyState.move3.moveName + "\n" +
    PartyState.move3.currentPP + " / " +
    PartyState.move3.maxPP;

move4Text.text =
    PartyState.move4.moveName + "\n" +
    PartyState.move4.currentPP + " / " +
    PartyState.move4.maxPP;
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

public void OnMove3Button()
{
    if (battleEnded) return;

    if (PartyState.move3.currentPP <= 0)
    {
        resultText.text = "PPがない！";
        return;
    }

    PartyState.move3.currentPP--;
    UpdateUI();
    StartCoroutine(PlayerAttackSequence(PartyState.move3));
}

public void OnMove4Button()
{
    if (battleEnded) return;

    if (PartyState.move4.currentPP <= 0)
    {
        resultText.text = "PPがない！";
        return;
    }

    PartyState.move4.currentPP--;
    UpdateUI();
    StartCoroutine(PlayerAttackSequence(PartyState.move4));
}

private IEnumerator PlayFireEffect()
{
    if (seAudioSource != null && fireSE != null)
{
    seAudioSource.PlayOneShot(fireSE);
}

    fireEffect.gameObject.SetActive(true);

    Vector3 startPos = fireEffect.transform.position;
    Vector3 targetPos = enemyImage.transform.position;

    Vector3 startScale = Vector3.one * 0.5f;
    Vector3 endScale = Vector3.one * 1.4f;

    fireEffect.transform.localScale = startScale;

    float time = 0f;
    float duration = 0.3f;

    while (time < duration)
    {
        time += Time.deltaTime;
        float t = time / duration;

        fireEffect.transform.position =
            Vector3.Lerp(startPos, targetPos, t);

        fireEffect.transform.localScale =
            Vector3.Lerp(startScale, endScale, t);

        yield return null;
    }

    fireEffect.gameObject.SetActive(false);

    fireEffect.transform.position = startPos;
    fireEffect.transform.localScale = Vector3.one;
}

private IEnumerator FlashEnemy()
{
    enemyImage.color = Color.red;
    yield return new WaitForSeconds(0.1f);
    enemyImage.color = Color.white;
}

private IEnumerator FadeOutBGM(AudioSource source, float duration)
{
    float startVolume = source.volume;
    float time = 0f;

    while (time < duration)
    {
        time += Time.deltaTime;
        source.volume = Mathf.Lerp(startVolume, 0f, time / duration);
        yield return null;
    }

    source.Stop();
    source.volume = startVolume;
}

private IEnumerator PlayScratchAttack()
{


    if (playerImage == null)
    {
        Debug.LogError("playerImage が Inspector に設定されていません！");
        yield break;
    }

    if (enemyImage == null)
    {
        Debug.LogError("enemyImage が Inspector に設定されていません！");
        yield break;
    }

    if (fireEffect == null || slashSprite == null)
    {
        Debug.LogError("fireEffect または slashSprite が設定されていません！");
        yield break;
    }
    Vector3 effectOriginalPos = fireEffect.transform.position;
Vector3 effectOriginalScale = fireEffect.transform.localScale;

    RectTransform playerRT = playerImage.rectTransform;

    Vector2 originalPos = playerRT.anchoredPosition;
    Vector3 originalScale = playerRT.localScale;

    Vector2 attackPos = originalPos + new Vector2(90f, 0f);

    StartAttackDark();
    StartGradient();
    StartSpeedLine();

    




    float time = 0f;
    float dashDuration = 0.08f;

    // 前進＋横伸び
    while (time < dashDuration)
    {
        time += Time.deltaTime;
        float t = time / dashDuration;

        playerRT.anchoredPosition =
            Vector2.Lerp(originalPos, attackPos, t);

        playerRT.localScale =
            Vector3.Lerp(
                originalScale,
                new Vector3(originalScale.x * 1.25f, originalScale.y * 0.85f, 1f),
                t);

        yield return null;
    }

    // 斬撃表示
if (seAudioSource != null && scratchSE != null)
{
    seAudioSource.PlayOneShot(scratchSE);
}




   if (enemyHP <= enemyMaxHP / 2)
{
    yield return StartCoroutine(PlayTripleSlashEffect());
}
else
{
    yield return StartCoroutine(PlaySingleSlashEffect());
}
yield return StartCoroutine(PlayShockwaveEffect());

yield return StartCoroutine(HitStop(0.05f));

yield return StartCoroutine(FlashEnemy());

    // 戻る
    time = 0f;
    float returnDuration = 0.12f;

    while (time < returnDuration)
    {
        time += Time.deltaTime;
        float t = time / returnDuration;

        playerRT.anchoredPosition =
            Vector2.Lerp(attackPos, originalPos, t);

        playerRT.localScale =
            Vector3.Lerp(
                new Vector3(originalScale.x * 1.25f, originalScale.y * 0.85f, 1f),
                originalScale,
                t);

        yield return null;
    }

    playerRT.anchoredPosition = originalPos;
    playerRT.localScale = originalScale;

    EndSpeedLine();
    EndGradient();
    EndAttackDark();

    fireEffect.transform.position = effectOriginalPos;
fireEffect.transform.localScale = effectOriginalScale;
}
private IEnumerator PlaySingleSlashEffect()
{
    fireEffect.sprite = slashSprite;
    fireEffect.gameObject.SetActive(true);

    fireEffect.transform.position = enemyImage.transform.position;
    fireEffect.transform.localScale = Vector3.one * 2.0f;

    yield return new WaitForSeconds(0.12f);

    fireEffect.gameObject.SetActive(false);
}
private IEnumerator PlayTripleSlashEffect()
{
    fireEffect.sprite = slashSprite;

    Vector3 centerPos = enemyImage.transform.position;

    Vector3[] offsets =
    {
        new Vector3(-30f, 20f, 0f),
        new Vector3(20f, 0f, 0f),
        new Vector3(-10f, -25f, 0f)
    };

    float[] scales =
    {
        1.5f,
        1.8f,
        2.2f
    };

    for (int i = 0; i < 3; i++)
    {
        fireEffect.gameObject.SetActive(true);
        fireEffect.transform.position = centerPos + offsets[i];
        fireEffect.transform.localScale = Vector3.one * scales[i];

        yield return new WaitForSeconds(0.05f);

        fireEffect.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.03f);
    }
}

private IEnumerator HitStop(float duration)
{
    Time.timeScale = 0f;

    yield return new WaitForSecondsRealtime(duration);

    Time.timeScale = 1f;
}

private void StartAttackDark()
{
    if (attackDarkPanel == null)
        return;

    attackDarkPanel.gameObject.SetActive(true);

    Color color = attackDarkPanel.color;
    color.a = 0.35f;
    attackDarkPanel.color = color;
}

private void EndAttackDark()
{
    if (attackDarkPanel == null)
        return;

    Color color = attackDarkPanel.color;
    color.a = 0f;
    attackDarkPanel.color = color;

    attackDarkPanel.gameObject.SetActive(false);
}
private void StartSpeedLine()
{
    foreach (SpeedLineScroller line in speedLines)
    {
        if (line != null)
            line.Show();
    }
}

private void EndSpeedLine()
{
    foreach (SpeedLineScroller line in speedLines)
    {
        if (line != null)
            line.Hide();
    }
}

private void StartGradient()
{
    if (battleGradient == null)
        return;

    battleGradient.gameObject.SetActive(true);

    Color c = battleGradient.color;
    c.a = 0.7f;

    battleGradient.color = c;
}

private void EndGradient()
{
    if (battleGradient == null)
        return;

    Color c = battleGradient.color;
    c.a = 0f;

    battleGradient.color = c;

    battleGradient.gameObject.SetActive(false);
}

private IEnumerator PlayHitExplosion()
{
    if (hitExplosion == null)
        yield break;

    hitExplosion.transform.position = enemyImage.transform.position;
    hitExplosion.transform.localScale = Vector3.one * 0.4f;

    Color c = hitExplosion.color;
    c.a = 1f;
    hitExplosion.color = c;

    hitExplosion.gameObject.SetActive(true);

    float time = 0f;
    float duration = 0.18f;

    while (time < duration)
    {
        time += Time.deltaTime;
        float t = time / duration;

        hitExplosion.transform.localScale =
            Vector3.Lerp(Vector3.one * 0.4f, Vector3.one * 1.8f, t);

        c.a = Mathf.Lerp(1f, 0f, t);
        hitExplosion.color = c;

        yield return null;
    }

    hitExplosion.gameObject.SetActive(false);
}


private IEnumerator PlayShockwaveEffect()
{
    if (shockwaveEffect == null || enemyImage == null)
        yield break;

    shockwaveEffect.transform.position = enemyImage.transform.position;
    shockwaveEffect.transform.localScale = Vector3.one * 0.15f;

    Color c = shockwaveEffect.color;
    c.a = 0.9f;
    shockwaveEffect.color = c;

    shockwaveEffect.gameObject.SetActive(true);

    float time = 0f;
    float duration = 0.2f;

    while (time < duration)
    {
        time += Time.deltaTime;
        float t = time / duration;

        shockwaveEffect.transform.localScale =
            Vector3.Lerp(Vector3.one * 0.15f, Vector3.one * 3.0f, t);

        c.a = Mathf.Lerp(0.9f, 0f, t);
        shockwaveEffect.color = c;

        yield return null;
    }

    shockwaveEffect.gameObject.SetActive(false);
}

}

