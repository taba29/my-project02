using UnityEngine;

using TMPro;

using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID && !UNITY_EDITOR
using Firebase;
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
    data.level = PartyState.level;
    data.victoryCount = PlayerProfileState.victoryCount;
    data.defeatCount = PlayerProfileState.defeatCount;

    data.currentHP = PartyState.currentHP;
data.maxHP = PartyState.maxHP;
data.exp = PartyState.exp;
data.nextLevelExp = PartyState.nextLevelExp;

    Debug.Log("保存するHP: " + data.currentHP + " / " + data.maxHP);
    Debug.Log("保存するEXP: " + data.exp + " / " + data.nextLevelExp);

    SavePlayer(data);
}

public void SavePlayer(PlayerData data)
{
    Debug.Log("SavePlayer 開始");

#if UNITY_ANDROID && !UNITY_EDITOR

    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
Debug.Log("Firestore Instance OK");

Dictionary<string, object> player = new Dictionary<string, object>()
{
    { "playerName", data.playerName },
    { "level", data.level },
    { "victoryCount", data.victoryCount },
    { "defeatCount", data.defeatCount },

    { "currentHP", data.currentHP },
    { "maxHP", data.maxHP },
    { "exp", data.exp },
    { "nextLevelExp", data.nextLevelExp }
};

Debug.Log("Dictionary OK");
Debug.Log("Before SetAsync");


db.Collection("players")
  .Document(data.playerName)
  .SetAsync(player)
  .ContinueWithOnMainThread(task =>
{
    Debug.Log("Continue reached");

    if (task.IsCompletedSuccessfully)
    {
        Debug.Log("Firestore Save Success");
    }
    else
    {
        Debug.LogError("Firestore Save Failed");
        Debug.LogError(task.Exception);
    }
});

Debug.Log("After SetAsync");

#endif
}

public void LoadPlayer(string playerName)
{
    Debug.Log("LoadPlayer 開始: " + playerName);

#if UNITY_ANDROID && !UNITY_EDITOR

    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    Debug.Log("GetSnapshotAsync 呼び出し前");

    db.Collection("players")
      .Document(playerName)
      .GetSnapshotAsync()
      .ContinueWithOnMainThread(task =>
      {
          Debug.Log("ContinueWithOnMainThread 開始");

          try
          {
              if (!task.IsCompletedSuccessfully)
{
    Debug.LogError("読込失敗");
    Debug.LogError(task.Exception);

    CommunicationSceneController controller =
        FindObjectOfType<CommunicationSceneController>();

    if (controller != null)
    {
        controller.ShowMessage("クラウド読込失敗");
    }

    return;
}

              Debug.Log("task成功");

              DocumentSnapshot snapshot = task.Result;

              if (!snapshot.Exists)
              {
                  Debug.Log("プレイヤーデータがありません: " + playerName);
                  return;
              }

              Debug.Log("snapshot存在あり");

              Dictionary<string, object> data = snapshot.ToDictionary();

              PlayerProfileState.playerName = data["playerName"].ToString();
              PlayerProfileState.level = int.Parse(data["level"].ToString());
              PlayerProfileState.victoryCount = int.Parse(data["victoryCount"].ToString());
              PlayerProfileState.defeatCount = int.Parse(data["defeatCount"].ToString());

              PartyState.currentHP = int.Parse(data["currentHP"].ToString());
PartyState.maxHP = int.Parse(data["maxHP"].ToString());
PartyState.exp = int.Parse(data["exp"].ToString());
PartyState.nextLevelExp = int.Parse(data["nextLevelExp"].ToString());

              PartyState.level = PlayerProfileState.level;

              Debug.Log("プレイヤー読込成功: " + PlayerProfileState.playerName);
              Debug.Log("読込レベル PlayerProfileState = " + PlayerProfileState.level);
              Debug.Log("読込レベル PartyState = " + PartyState.level);

              CommunicationSceneController controller2 =
    FindObjectOfType<CommunicationSceneController>();

if (controller2 != null)
{
    controller2.ShowMessage("クラウド読込完了！");
};


          }
          catch (System.Exception e)
          {
              Debug.LogError("LoadPlayer内で例外");
              Debug.LogError(e);
          }
      });

    Debug.Log("GetSnapshotAsync 呼び出し後");

#endif
}


public void LoadAllPlayers()
{
    StartCoroutine(LoadAllPlayersCoroutine());
}

private IEnumerator LoadAllPlayersCoroutine()
{
    Debug.Log("LoadAllPlayers Coroutine 開始");

#if UNITY_ANDROID && !UNITY_EDITOR

    if (!FirebaseInitializer.IsInitialized)
    {
        Debug.LogError("Firebase not ready");
        yield break;
    }

    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
Debug.Log("db = " + db);

CollectionReference col = db.Collection("players");
Debug.Log("Collection = " + col);

Debug.Log("GetSnapshotAsync 作成");

var task = col.GetSnapshotAsync();

    float startTime = Time.realtimeSinceStartup;

while (!task.IsCompleted && Time.realtimeSinceStartup - startTime < 10f)
{
    Debug.Log("timer = " + (Time.realtimeSinceStartup - startTime));
    yield return null;
}

Debug.Log("while終了");
Debug.Log("task.IsCompleted = " + task.IsCompleted);
Debug.Log("task.IsFaulted = " + task.IsFaulted);
Debug.Log("task.IsCanceled = " + task.IsCanceled);

    if (!task.IsCompleted)
    {
        Debug.LogError("Firestore timeout");
        FindObjectOfType<CommunicationSceneController>()?.ShowMessage("Firestore timeout");
        yield break;
    }

    if (!task.IsCompletedSuccessfully)
    {
        Debug.LogError("Firestore failed");
        Debug.LogError(task.Exception);
        FindObjectOfType<CommunicationSceneController>()?.ShowMessage("Player list failed");
        yield break;
    }

    Debug.Log("Firestore success");

QuerySnapshot snapshot = task.Result;

string result = "Player List\n\n";

int count = 0;

foreach (DocumentSnapshot doc in snapshot.Documents)
{
    count++;

    Debug.Log("Document = " + doc.Id);

    Dictionary<string, object> data = doc.ToDictionary();

    if (!data.ContainsKey("playerName") || !data.ContainsKey("level"))
    {
        Debug.LogError("必要なキーがないDocument: " + doc.Id);
        continue;
    }

    Debug.Log("playerName = " + data["playerName"]);
    Debug.Log("level = " + data["level"]);

    CommunicationSceneController controller =
    FindObjectOfType<CommunicationSceneController>();

GameObject row = controller.CreatePlayerRow();

TMP_Text text = row.GetComponentInChildren<TMP_Text>();

text.text = data["playerName"] + " Lv" + data["level"];
}

Debug.Log("Document Count = " + count);



    
#endif

yield break;
}

private void Start()
{
#if UNITY_ANDROID && !UNITY_EDITOR
   // StartCoroutine(TestSave());
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

    yield return new WaitForSeconds(1f);

    LoadPlayer("Player");
}
}