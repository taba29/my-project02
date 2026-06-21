using UnityEngine;

#if UNITY_ANDROID && !UNITY_EDITOR
using Firebase;
using Firebase.Extensions;
#endif

public class FirebaseInitializer : MonoBehaviour
{
    public static bool IsInitialized { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID && !UNITY_EDITOR
        FirebaseApp.CheckAndFixDependenciesAsync()
            .ContinueWithOnMainThread(task =>
            {
                var status = task.Result;

                if (status == DependencyStatus.Available)
                {
                    IsInitialized = true;
                    Debug.Log("Firebase 初期化成功");
                }
                else
                {
                    Debug.LogError("Firebase 初期化失敗 : " + status);
                }
            });
#else
        IsInitialized = false;
        Debug.Log("Firebase 初期化スキップ：Unity Editorでは実行しません");
#endif
    }
}