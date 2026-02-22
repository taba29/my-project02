using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuStickNavigator : MonoBehaviour
{
    [Header("Input")]
    public VirtualStick stick;
    public PressButton btnA;
    public PressButton btnB;

    [Header("Move")]
    public float moveThreshold = 0.55f;
    public float repeatDelay = 0.22f;
    float timer;

    [Header("Preview (optional)")]
    public PauseMenu pauseMenu;

    [Header("Visual")]
    public float selectedScale = 1.06f; // 選択中の拡大率

    Button[] items;
    int index;

    void Awake()
{
    // ★ MenuButtons（このオブジェクト）の「直下」だけ拾う
    var list = new System.Collections.Generic.List<Button>();
    for (int i = 0; i < transform.childCount; i++)
    {
        var child = transform.GetChild(i);
        var b = child.GetComponent<Button>();
        if (b != null) list.Add(b);
    }
    items = list.ToArray();

    if (items == null || items.Length == 0) return;

    index = 0;
    Select(index);
}
    void Update()
    {
        if (items == null || items.Length == 0) return;

        timer -= Time.unscaledDeltaTime;

        // ---- スティック上下で選択移動 ----
        float v = (stick != null) ? stick.Value.y : Input.GetAxisRaw("Vertical");

        if (timer <= 0f)
        {
            if (v > moveThreshold)
            {
                Move(up: true);
                timer = repeatDelay;
            }
            else if (v < -moveThreshold)
            {
                Move(up: false);
                timer = repeatDelay;
            }
        }

        // ---- Aで決定 ----
        bool aDown = Input.GetKeyDown(KeyCode.Space);
        if (btnA != null) aDown |= btnA.Down;

        if (aDown)
        {
            // クリックと同じ＝一番堅い
            items[index].onClick.Invoke();
        }

        // ---- Bで閉じる ----
        bool bDown = Input.GetKeyDown(KeyCode.Escape);
        if (btnB != null) bDown |= btnB.Down;

        if (bDown && pauseMenu != null)
        {
            pauseMenu.Toggle();
        }
    }

    void Move(bool up)
    {
        int next = up ? index - 1 : index + 1;
        if (next < 0) next = items.Length - 1;
        if (next >= items.Length) next = 0;

        index = next;
        Select(index);
    }

    void Select(int i)
    {
        // EventSystem上の選択も合わせる（安定）
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(items[i].gameObject);

        items[i].Select();     // Buttonの選択状態（任意だけど安定）
        UpdateVisual(i);       // ★RPG見た目

        if (pauseMenu != null)
            pauseMenu.PreviewBySelected(items[i].gameObject);
    }

    void UpdateVisual(int selectedIndex)
    {
        for (int k = 0; k < items.Length; k++)
        {
            bool on = (k == selectedIndex);

            // 1) 背景ハイライト
            var bg = items[k].transform.Find("SelectedBG");
            if (bg != null) bg.gameObject.SetActive(on);

            // 2) 矢印
            var arrow = items[k].transform.Find("SelectedArrow");
            if (arrow != null) arrow.gameObject.SetActive(on);

            // 3) 拡大（レイアウト崩れるなら ScaleRoot に切替）
            var scaleRoot = items[k].transform.Find("ScaleRoot");
            if (scaleRoot != null)
                scaleRoot.localScale = on ? Vector3.one * selectedScale : Vector3.one;
            else
                items[k].transform.localScale = on ? Vector3.one * selectedScale : Vector3.one;
        }
    }

    public void SelectByGameObject(GameObject go)
{
    if (items == null || items.Length == 0 || go == null) return;

    for (int i = 0; i < items.Length; i++)
    {
        if (items[i] != null && items[i].gameObject == go)
        {
            index = i;
            Select(index);
            break;
        }
    }
}
public void SelectByButton(Button b)
{
    if (items == null || items.Length == 0 || b == null) return;

    Debug.Log("SelectByButton: " + b.name);

    for (int i = 0; i < items.Length; i++)
    {
        if (items[i] == b)
        {
            Debug.Log("Matched index=" + i);
            index = i;
            Select(index);
            return;
        }
    }

    Debug.LogWarning("NOT FOUND in items: " + b.name);
}
}