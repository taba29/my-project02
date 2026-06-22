using UnityEngine;

using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID && !UNITY_EDITOR
using Firebase.Firestore;
using Firebase.Extensions;
#endif

public class FirestoreManager : MonoBehaviour
{
    public static FirestoreManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

   public void SaveCurrentPlayer()
{
    Debug.Log("SaveCurrentPlayer 開始");

    PlayerData data = new PlayerData();

    data.playerName = PlayerProfileState.playerName;
    data.level = PlayerProfileState.level;
    data.victoryCount = PlayerProfileState.victoryCount;
    data.defeatCount = PlayerProfileState.defeatCount;

    Debug.Log("保存する名前: " + data.playerName);

    SavePlayer(data);
}

public void SavePlayer(PlayerData data)
{
    Debug.Log("SavePlayer 開始");

#if UNITY_ANDROID && !UNITY_EDITOR

    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    Dictionary<string, object> player = new Dictionary<string, object>()
    {
        { "playerName", data.playerName },
        { "level", data.level },
        { "victoryCount", data.victoryCount },
        { "defeatCount", data.defeatCount }
    };

    db.Collection("players")
      .Document(data.playerName)
      .SetAsync(player)
      .ContinueWithOnMainThread(task =>
      {
          if (task.IsCompletedSuccessfully)
          {
              Debug.Log("プレイヤー保存成功");
          }
          else
          {
              Debug.LogError("プレイヤー保存失敗: " + task.Exception);
          }
      });

#endif
}











private void Start()
{
#if UNITY_ANDROID && !UNITY_EDITOR
    StartCoroutine(TestSave());
#endif
}
private IEnumerator TestSave()
{
    Debug.Log("FirestoreManager TestSave開始");

    while (!FirebaseInitializer.IsInitialized)
    {
        Debug.Log("FirestoreManager Firebase初期化待ち");
        yield return new WaitForSeconds(0.5f);
    }

    Debug.Log("FirestoreManager Firebase初期化OK");

    SaveCurrentPlayer();
}
}