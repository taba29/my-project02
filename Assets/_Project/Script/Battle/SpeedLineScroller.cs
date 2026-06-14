using UnityEngine;
using UnityEngine.UI;

public class SpeedLineScroller : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float speed = 1000f;
    [SerializeField] private float loopX = 1920f;
    [SerializeField] private float startX = -2200f;
[SerializeField] private float endX = 2200f;

    private Vector2 startPos;

    private void Awake()
    {
        if (rect == null)
            rect = GetComponent<RectTransform>();

        startPos = rect.anchoredPosition;
    }

    private void Update()
    {
        rect.anchoredPosition += Vector2.right * speed * Time.deltaTime;

if (rect.anchoredPosition.x > endX)
{
    rect.anchoredPosition =
        new Vector2(startX, rect.anchoredPosition.y);
}
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

