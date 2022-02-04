using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUIPanel : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.name == "Shop_Panel")
            gameObject.SetActive(false);
    }
}