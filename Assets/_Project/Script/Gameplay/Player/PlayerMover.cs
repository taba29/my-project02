using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private VirtualStick stick;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private LayerMask blockLayer;

    private bool isMoving = false;
    private Vector3 targetPosition;

    public Vector2Int FacingDirection { get; private set; } = Vector2Int.down;
    public bool IsMoving => isMoving;
    public float CellSize => cellSize;

    void Start()
    {
        SnapToGridCenter();
        targetPosition = transform.position;
    }

    void Update()
    {

        if (dialogueManager != null && dialogueManager.IsOpen)
{
    return;
}

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
        Vector2Int dir = GetCardinalInput(input);

        if (dir == Vector2Int.zero) return;

        FacingDirection = dir;

        Vector3 nextPosition = transform.position + new Vector3(
    dir.x * cellSize,
    dir.y * cellSize,
    0f
);

if (IsBlocked(nextPosition))
{
    return;
}

targetPosition = nextPosition;
isMoving = true;
    }

    private Vector2Int GetCardinalInput(Vector2 input)
    {
        if (input.magnitude < 0.5f) return Vector2Int.zero;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            return input.x > 0 ? Vector2Int.right : Vector2Int.left;
        }
        else
        {
            return input.y > 0 ? Vector2Int.up : Vector2Int.down;
        }
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

    private bool IsBlocked(Vector3 nextPosition)
{
    Vector2 checkSize = Vector2.one * (cellSize * 0.8f);

    Collider2D hit = Physics2D.OverlapBox(
        nextPosition,
        checkSize,
        0f,
        blockLayer
    );

    return hit != null;
}
}