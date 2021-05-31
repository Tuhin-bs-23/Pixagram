using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject heartImage;
    void Start()
    {
        heartImage.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("Double Click");
            heartImage.SetActive(true);
            eventData.clickCount = 0;
        }
    }
}