using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        var dir = new Vector3(x, y, 0f).normalized;

        transform.position += dir * speed * Time.deltaTime;
    }
}