using UnityEngine;

public class MapBGMStarter : MonoBehaviour
{
    [SerializeField] private AudioClip mapBGM;

    private void Start()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBGM(mapBGM);
        }
    }
}