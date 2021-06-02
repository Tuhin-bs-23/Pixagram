using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour//, IPointerClickHandler
{
    public GameObject heartImage;
    int i;
    void Start()
    {
        heartImage.SetActive(false);
        i = 0;
    }
    public void DoubleTab()
    {
        i++;
        if (i==2)
        {
            heartImage.SetActive(true);
            i = 0;
        }
        
    }
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("Click Count  "+eventData.clickCount);
    //    if (eventData.clickCount == 2)
    //    {
    //        Debug.Log("Double Click");
    //        heartImage.SetActive(true);
    //        eventData.clickCount = 0;
    //    }
    //}
}