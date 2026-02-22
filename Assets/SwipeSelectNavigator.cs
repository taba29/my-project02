using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeSelectNavigator : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerClickHandler
{
    [Header("Swipe")]
    public float swipeThreshold = 40f;

    [Header("First Select (optional)")]
    public Selectable firstSelectable; // ここに最初のボタンを入れると安定

    Vector2 downPos;
    bool swiped;

    void Start()
    {
        // 初期選択（First Selected 代わり）
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == null)
        {
            var sel = firstSelectable != null ? firstSelectable : FindFirstSelectable();
            if (sel != null) EventSystem.current.SetSelectedGameObject(sel.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("TouchPad Down");
        downPos = eventData.position;
        swiped = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("TouchPad Drag");

        if (swiped) return;

        Vector2 delta = eventData.position - downPos;
        if (delta.magnitude < swipeThreshold) return;

        swiped = true;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            Move(delta.x > 0 ? Dir.Right : Dir.Left);
        else
            Move(delta.y > 0 ? Dir.Up : Dir.Down);
    }

    public void OnPointerUp(PointerEventData eventData) { }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (swiped) return;

        var go = EventSystem.current?.currentSelectedGameObject;
        if (go == null) return;

        var btn = go.GetComponent<Button>();
        if (btn != null && btn.interactable) btn.onClick.Invoke();
    }

    enum Dir { Up, Down, Left, Right }

    void Move(Dir dir)
    {
        var es = EventSystem.current;
        if (es == null) return;

        GameObject curGO = es.currentSelectedGameObject;
        Selectable cur = curGO != null ? curGO.GetComponent<Selectable>() : null;

        if (cur == null)
        {
            var sel = firstSelectable != null ? firstSelectable : FindFirstSelectable();
            if (sel != null) es.SetSelectedGameObject(sel.gameObject);
            return;
        }

        Selectable next =
            dir == Dir.Up ? cur.FindSelectableOnUp() :
            dir == Dir.Down ? cur.FindSelectableOnDown() :
            dir == Dir.Left ? cur.FindSelectableOnLeft() :
            cur.FindSelectableOnRight();

        if (next != null) es.SetSelectedGameObject(next.gameObject);
    }

    Selectable FindFirstSelectable()
    {
        return GameObject.FindObjectOfType<Selectable>();
    }
}
