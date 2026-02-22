using UnityEngine;
using UnityEngine.EventSystems;

public class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPressing { get; private set; }
    public bool Down { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressing = true;
        Down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressing = false;
    }

    void LateUpdate()
    {
        // 1フレームだけ true になる「押した瞬間」
        Down = false;
    }
}
