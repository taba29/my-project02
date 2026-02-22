using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        Rect safe = Screen.safeArea;

        Vector2 min = safe.position;
        Vector2 max = safe.position + safe.size;

        min.x /= Screen.width;
        min.y /= Screen.height;
        max.x /= Screen.width;
        max.y /= Screen.height;

        rt.anchorMin = min;
        rt.anchorMax = max;

        Debug.Log($"safeArea={Screen.safeArea} screen={Screen.width}x{Screen.height} anchors {rt.anchorMin} - {rt.anchorMax}");

    }

    void OnRectTransformDimensionsChange()
{
    if (rt == null) rt = GetComponent<RectTransform>();
    ApplySafeArea();
}

}
