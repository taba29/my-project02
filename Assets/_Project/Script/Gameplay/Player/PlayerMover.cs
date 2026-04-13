using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private VirtualStick stick;

    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        SnapToGridCenter();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }

            return;
        }

        if (stick == null) return;

        Vector2 input = stick.Value;
        Debug.Log($"stick.Value = {input}");

        Vector2Int dir = GetCardinalInput(input);
        Debug.Log($"dir = {dir}");

        if (dir == Vector2Int.zero) return;

        targetPosition = transform.position + new Vector3(
            dir.x * cellSize,
            dir.y * cellSize,
            0f
        );

        isMoving = true;
    }

    private Vector2Int GetCardinalInput(Vector2 input)
    {
        if (input.magnitude < 0.5f) return Vector2Int.zero;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return input.x > 0 ? Vector2Int.right : Vector2Int.left;
        else
            return input.y > 0 ? Vector2Int.up : Vector2Int.down;
    }

    private void SnapToGridCenter()
    {
        Vector3 p = transform.position;
        transform.position = new Vector3(
            Mathf.Round(p.x / cellSize) * cellSize,
            Mathf.Round(p.y / cellSize) * cellSize,
            p.z
        );
    }
}