using UnityEngine;

public class PlayerStartPositionRecorder : MonoBehaviour
{
    private void Start()
    {
        if (!BattleState.hasInitialPlayerPosition)
        {
            BattleState.initialPlayerPosition = transform.position;
            BattleState.hasInitialPlayerPosition = true;
        }
    }
}