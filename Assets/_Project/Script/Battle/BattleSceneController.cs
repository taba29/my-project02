using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BattleSceneController : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Background")]
    [SerializeField] private Image backgroundImage;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerHPText;
    [SerializeField] private TextMeshProUGUI enemyHPText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button backButton;

    [Header("Enemy View")]
    [SerializeField] private Image enemyImage;
    [SerializeField] private float shakeDistance = 20f;
    [SerializeField] private float shakeTime = 0.2f;

    [Header("Player Status")]
    [SerializeField] private int playerHP = 30;
    [SerializeField] private int playerAttackPower = 5;

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
        backButton.gameObject.SetActive(false);

        StartCoroutine(FadeIn());
    }

    private void LoadEnemyDataFromState()
    {
        enemyName = BattleState.currentEnemyName;
        enemyHP = BattleState.currentEnemyHP;
        enemyAttackPower = BattleState.currentEnemyAttackPower;

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
        if (battleEnded) return;
        StartCoroutine(PlayerAttackSequence());
    }

    private IEnumerator PlayerAttackSequence()
    {
        attackButton.interactable = false;

        resultText.text = "Player Attack!";
        yield return new WaitForSeconds(0.3f);

        if (enemyImage != null)
        {
            yield return StartCoroutine(ShakeEnemy());
        }

        enemyHP -= playerAttackPower;
        if (enemyHP < 0) enemyHP = 0;
        UpdateHPText();

        resultText.text = enemyName + " took " + playerAttackPower + " damage!";
        yield return new WaitForSeconds(0.7f);

        if (enemyHP <= 0)
        {
            battleEnded = true;
            BattleState.playerWon = true;
            BattleState.playerDefeated = false;

            resultText.text = "Victory!";
            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadScene("Map01");
            yield break;
        }

        resultText.text = enemyName + " Attack!";
        yield return new WaitForSeconds(0.5f);

        playerHP -= enemyAttackPower;
        if (playerHP < 0) playerHP = 0;
        UpdateHPText();

        resultText.text = "Player took " + enemyAttackPower + " damage!";
        yield return new WaitForSeconds(0.7f);

        if (playerHP <= 0)
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
        attackButton.interactable = true;
    }

    public void BackToMap()
    {
        SceneManager.LoadScene("Map01");
    }

    private void UpdateHPText()
    {
        playerHPText.text = "Player HP: " + playerHP;
        enemyHPText.text = enemyName + " HP: " + enemyHP;
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
}