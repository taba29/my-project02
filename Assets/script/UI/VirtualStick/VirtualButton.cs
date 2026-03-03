using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsHeld { get; private set; }
    public bool DownThisFrame { get; private set; }

    void LateUpdate() => DownThisFrame = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsHeld = true;
        DownThisFrame = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsHeld = false;
    }
}
