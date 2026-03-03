using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItemTapSelect : MonoBehaviour, IPointerDownHandler
{
    public MenuStickNavigator navigator; // MenuButtons の MenuStickNavigator を入れる
    private Button self;

    void Awake()
    {
        self = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown: " + gameObject.name);

        if (navigator != null && self != null)
            navigator.SelectByButton(self);
    }
}