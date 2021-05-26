using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerDownHandler
{
    public GameObject heartImage;
    void Start()
    {
        heartImage.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.clickCount >= 2)
        {
            Debug.Log("Double Click");
            heartImage.SetActive(true);
            eventData.clickCount = 0;
        }
    }
}