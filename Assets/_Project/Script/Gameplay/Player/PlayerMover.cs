using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] VirtualStickDpad stick;   // ★型を変更

    void Update()
    {
        if (stick == null) return;

        Vector2 input = stick.Value; // -1..1（snapToCardinalなら(0,±1)(±1,0)）
        Vector3 dir = new Vector3(input.x, input.y, 0f);

        transform.position += dir * speed * Time.deltaTime;
    }
}