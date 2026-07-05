using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRowButton : MonoBehaviour
{
    private PlayerData playerData;

    [SerializeField] private TMP_Text playerNameText;

    public void Setup(PlayerData data)
    {
        playerData = data;

        playerNameText.text = data.playerName + " Lv" + data.level;
    }

    public void OnClick()
    {
        SelectedPlayerState.playerName = playerData.playerName;
        SelectedPlayerState.level = playerData.level;
        SelectedPlayerState.victoryCount = playerData.victoryCount;
        SelectedPlayerState.defeatCount = playerData.defeatCount;
        SelectedPlayerState.currentHP = playerData.currentHP;
        SelectedPlayerState.maxHP = playerData.maxHP;
        SelectedPlayerState.exp = playerData.exp;
        SelectedPlayerState.nextLevelExp = playerData.nextLevelExp;

        Debug.Log("選択したプレイヤー: " + SelectedPlayerState.playerName);
    }
}