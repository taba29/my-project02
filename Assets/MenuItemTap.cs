using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItemTap : MonoBehaviour, IPointerDownHandler
{
    public MenuStickNavigator nav;

    public void OnPointerDown(PointerEventData eventData)
    {
        var b = GetComponent<Button>();
        if (nav != null && b != null) nav.SelectByButton(b);
    }
}