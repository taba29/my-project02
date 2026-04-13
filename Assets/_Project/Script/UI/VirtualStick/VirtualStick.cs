using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform knob;   // StickKnob
    [SerializeField] private float radius = 150f;
    public Vector2 Value { get; private set; }

    private RectTransform rt;
    private Canvas canvas;
    private Camera uiCam;

    void Awake()
    {
        rt = (RectTransform)transform;
        canvas = GetComponentInParent<Canvas>();
        uiCam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        ResetKnob();
    }

    public void OnPointerDown(PointerEventData e)
    {
        OnDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, e.position, uiCam, out var local))
            return;

        // Rectの中心基準に変換する
        Vector2 centerOffset = local - rt.rect.center;

        // 半径内に収める
        Vector2 clamped = Vector2.ClampMagnitude(centerOffset, radius);

        // ノブ移動
        knob.anchoredPosition = clamped;

        // -1 ～ 1
        Value = clamped / radius;

        Debug.Log($"local={local} centerOffset={centerOffset} value={Value}");
    }

    public void OnPointerUp(PointerEventData e)
    {
        Value = Vector2.zero;
        ResetKnob();
    }

    private void ResetKnob()
    {
        if (knob != null)
            knob.anchoredPosition = Vector2.zero;
    }
}