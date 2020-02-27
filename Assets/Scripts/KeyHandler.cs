using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyHandler : MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler, 
    IPointerClickHandler
{


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Klick");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Image img = this.gameObject.GetComponent<Image>();
        img.color = new Color32(30, 40, 55,255);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Image img = this.gameObject.GetComponent<Image>();
        img.color = Color.grey;
    }
}
