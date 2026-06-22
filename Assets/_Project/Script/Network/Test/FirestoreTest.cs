using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;

public class FirestoreTest : MonoBehaviour
{
    private IEnumerator Start()
    {
        Debug.Log("FirestoreTest Start");

#if UNITY_ANDROID && !UNITY_EDITOR
        while (!FirebaseInitializer.IsInitialized)
        {
            Debug.Log("Firebase初期化待ち...");
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Firebase初期化確認OK");

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        Dictionary<string, object> player = new Dictionary<string, object>()
        {
            { "name", "Taba" },
            { "level", 1 },
            { "victory", 0 }
        };

        Debug.Log("Firestore 書き込み開始");

        db.Collection("players")
          .Document("test")
          .SetAsync(player)
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsCompletedSuccessfully)
              {
                  Debug.Log("Firestore 保存成功 players/test");
              }
              else
              {
                  Debug.LogError("Firestore 保存失敗: " + task.Exception);
              }
          });
#else
        Debug.Log("Firestore テストはEditorでは実行しません");
        yield break;
#endif
    }
}