using UnityEngine;

public class BattleBGM : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private float loopStart = 18f;
    private float loopEnd = 150f;

    void Update()
    {
        if (source.time >= loopEnd)
        {
            source.time = loopStart;
        }
    }
}