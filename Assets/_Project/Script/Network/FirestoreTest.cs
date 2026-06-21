using UnityEngine;

#if UNITY_ANDROID && !UNITY_EDITOR
using Firebase.Firestore;
#endif

public class FirestoreTest : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Firestore 接続成功！");
#else
        Debug.Log("Firestore 接続テストはUnity Editorでは実行しません");
#endif
    }
}