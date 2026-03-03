using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; }
    public GameState State { get; private set; } = GameState.Title;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        
    }

    public void SetState(GameState s) => State = s;
}