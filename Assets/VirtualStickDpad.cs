using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualStickDpad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public enum Dir { None, Up, Down, Left, Right }

    [Header("Visual")]
    [SerializeField] RectTransform knob;  // StickKnob
    [SerializeField] float radius = 150f; // BG半径
    [SerializeField] float deadZone = 18f; // 中央誤爆防止

    [Header("Snap")]
    public bool snapToCardinal = true;    // 4方向にスナップ（RPGメニュー向け）

    public Vector2 Value { get; private set; } // -1..1（互換用）
    public Dir CurrentDir { get; private set; } = Dir.None;

    public bool IsHeld { get; private set; }
    public bool DownThisFrame { get; private set; } // 押した瞬間1フレ

    RectTransform rt;
    Canvas canvas;
    Camera uiCam;

    void Awake()
    {
        rt = (RectTransform)transform;
        canvas = GetComponentInParent<Canvas>();
        uiCam = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.worldCamera : null;
        ResetKnob();
    }

    void LateUpdate()
    {
        DownThisFrame = false;
    }

    public void OnPointerDown(PointerEventData e)
    {
        IsHeld = true;
        DownThisFrame = true;
        ApplyFromScreenPoint(e.position);
    }

    public void OnDrag(PointerEventData e)
    {
        if (!IsHeld) return;
        ApplyFromScreenPoint(e.position);
    }

    public void OnPointerUp(PointerEventData e)
    {
        IsHeld = false;
        CurrentDir = Dir.None;
        Value = Vector2.zero;
        ResetKnob();
    }

    void ApplyFromScreenPoint(Vector2 screenPos)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenPos, uiCam, out var local))
            return;
            local -= rt.rect.center;   // ★中心基準に補正（超重要）
            
        // 範囲内を触った場所に“触れてる感”を出す（視覚優先）
        var clamped = Vector2.ClampMagnitude(local, radius);

        // 中央付近は無効（誤爆防止）
        if (clamped.magnitude < deadZone)
        {
            CurrentDir = Dir.None;
            Value = Vector2.zero;
            knob.anchoredPosition = Vector2.zero;
            return;
        }

        // 方向を決める（4分割）
        Dir dir = GetDir(clamped);

        if (snapToCardinal)
        {
            // ノブを方向へスナップ（見た目が分かりやすい）
            clamped = DirToPos(dir) * radius;
        }

        knob.anchoredPosition = clamped;
        Value = clamped / radius;
        CurrentDir = dir;
    }

    Dir GetDir(Vector2 v)
    {
        if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
            return v.x > 0 ? Dir.Right : Dir.Left;
        else
            return v.y > 0 ? Dir.Up : Dir.Down;
    }

    Vector2 DirToPos(Dir d)
    {
        return d == Dir.Up ? Vector2.up :
               d == Dir.Down ? Vector2.down :
               d == Dir.Left ? Vector2.left :
               d == Dir.Right ? Vector2.right :
               Vector2.zero;
    }

    void ResetKnob()
    {
        if (knob) knob.anchoredPosition = Vector2.zero;
    }
}