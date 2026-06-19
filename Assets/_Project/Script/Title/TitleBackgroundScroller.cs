using UnityEngine;

public class TitleBackgroundScroller : MonoBehaviour
{
    [SerializeField] private RectTransform scrollContent;
    [SerializeField] private float scrollSpeed = 80f;
    [SerializeField] private float loopHeight = 5760f;

    void Update()
    {
        scrollContent.anchoredPosition -= Vector2.down * scrollSpeed * Time.deltaTime;

        if (scrollContent.anchoredPosition.y >= loopHeight)
        {
            scrollContent.anchoredPosition = Vector2.zero;
        }
    }
}
