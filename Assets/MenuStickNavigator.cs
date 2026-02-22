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

    [Header("Dpad Repeat")]
public float firstRepeatDelay = 0.35f; // 長押し開始まで
public float repeatInterval = 0.18f;   // リピート間隔
float holdTimer;
float repTimer;

[Header("Dpad Stick (replace VirtualStick)")]
public VirtualStickDpad dpad; // ★ VirtualStick の代わり

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

    // ---- 4方向タップ/長押し（StickBG内） ----
    if (dpad != null)
    {
        var dir = dpad.CurrentDir;

        

        // 短タップ：押した瞬間に1回
        if (dpad.DownThisFrame && dir != VirtualStickDpad.Dir.None)
        {
            Step(dir);
            holdTimer = 0f;
            repTimer = firstRepeatDelay;
        }

        // 長押し：押してる間リピート
        if (dpad.IsHeld && dir != VirtualStickDpad.Dir.None)
        {
            holdTimer += Time.unscaledDeltaTime;
            repTimer -= Time.unscaledDeltaTime;

            // firstRepeatDelay 以降、repeatIntervalで連打
            if (holdTimer >= firstRepeatDelay && repTimer <= 0f)
            {
                Step(dir);
                repTimer = repeatInterval;
            }
        }
    }

    // ---- Aで決定 ----
    bool aDown = Input.GetKeyDown(KeyCode.Space);
    if (btnA != null) aDown |= btnA.Down;
    if (aDown) items[index].onClick.Invoke();

    // ---- Bで閉じる ----
    bool bDown = Input.GetKeyDown(KeyCode.Escape);
    if (btnB != null) bDown |= btnB.Down;
    if (bDown && pauseMenu != null) pauseMenu.Toggle();
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
void Step(VirtualStickDpad.Dir dir)
{
    if (items == null || items.Length == 0) return;

    // ★メニューの上下は自前のindexで確実に動かす
    if (dir == VirtualStickDpad.Dir.Up)   { Move(up: true);  return; }
    if (dir == VirtualStickDpad.Dir.Down) { Move(up: false); return; }

    // 左右は（今は）無視 or 後でタブ切り替えに使う
    if (dir == VirtualStickDpad.Dir.Left)
    {
        // TODO: タブ左へ（Party/Gear/Quest…）など
        return;
    }
    if (dir == VirtualStickDpad.Dir.Right)
    {
        // TODO: タブ右へ
        return;
    }
}
}

