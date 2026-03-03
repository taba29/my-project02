using UnityEngine;

public class BootLoader : MonoBehaviour
{
    void Start()
    {
        SceneLoader.I.Load("Game");
    }
}