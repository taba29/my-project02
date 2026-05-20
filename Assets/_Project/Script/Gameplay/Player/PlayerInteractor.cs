using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private VirtualButton actionButton;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private LayerMask interactLayer;

    void Awake()
    {
        if (playerMover == null)
            playerMover = GetComponent<PlayerMover>();
    }

    void Update()
    {
        if (actionButton == null) return;
        if (!actionButton.DownThisFrame) return;

        if (dialogueManager != null && dialogueManager.IsOpen)
        {
            dialogueManager.CloseDialogue();
            return;
        }

        TryInteract();
    }

    private void TryInteract()
    {
        if (playerMover == null) return;
        if (playerMover.IsMoving) return;

        Vector2 origin = transform.position;
        Vector2 dir = playerMover.FacingDirection;
        float distance = playerMover.CellSize;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            dir,
            distance,
            interactLayer
        );

        if (hit.collider == null) return;

        TreasureChest chest = hit.collider.GetComponent<TreasureChest>();

        if (chest != null)
        {
        if (dialogueManager != null)
            dialogueManager.ShowDialogue(chest.GetMessage());

        return;
        }

        TalkableNPC npc = hit.collider.GetComponent<TalkableNPC>();

        if (npc == null) return;

        if (dialogueManager != null)
            dialogueManager.ShowDialogue(npc.message);
    }
}