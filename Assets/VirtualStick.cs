using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] RectTransform knob;      // StickKnob
    [SerializeField] float radius = 150f;     // BGの半径くらい
    public Vector2 Value { get; private set; } // -1..1

    RectTransform rt;
    Canvas canvas;
    Camera uiCam;

    void Awake()
    {
        rt = (RectTransform)transform;
        canvas = GetComponentInParent<Canvas>();
        uiCam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        ResetKnob();
    }

    public void OnPointerDown(PointerEventData e) => OnDrag(e);

    public void OnDrag(PointerEventData e)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, e.position, uiCam, out var local))
            return;

        var clamped = Vector2.ClampMagnitude(local, radius);
        knob.anchoredPosition = clamped;
        Value = clamped / radius; // -1..1
    }

    public void OnPointerUp(PointerEventData e)
    {
        Value = Vector2.zero;
        ResetKnob();
    }

    void ResetKnob()
    {
        if (knob) knob.anchoredPosition = Vector2.zero;
    }
}
