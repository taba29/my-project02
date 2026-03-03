using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InputHub : MonoBehaviour
{
    public VirtualStick stick;
    public VirtualButton btnA;
    public VirtualButton btnB;

    void Update()
    {
        Vector2 move = stick.Value; // アナログ (-1..1)
        if (btnA.DownThisFrame) Debug.Log("A pressed");
        if (btnB.IsHeld) Debug.Log("B holding");

        // 例：移動
        // player.Move(new Vector3(move.x, 0, move.y));
    }
}
