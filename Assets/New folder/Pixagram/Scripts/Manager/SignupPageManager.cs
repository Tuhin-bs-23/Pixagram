using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

[Serializable]
public class ToggolMenu
{
    public GameObject toggolItem;
    public TextMeshProUGUI text;
    public Image image;
}
public class SignupPageManager : SerializedMonoBehaviour
{
    public TMP_InputField phoneNumber;
    public TMP_InputField email;

    public ToggolMenu[] toggolMenu;

    int toggolSelectedNo = 1;
    private void Start()
    {
        ToggoledAction(0);
    }

    public void ToggoledAction(int i)
    {
        foreach (var menu in toggolMenu)
        {
            menu.toggolItem.SetActive(false);
            menu.image.color = new Color(50f, 50f, 50f, 128f);
            menu.text.color = new Color(50f, 50f, 50f, 128f);
        }
        toggolMenu[i].toggolItem.SetActive(true);
        toggolMenu[i].image.color = new Color(0, 0, 0, 255f);
        toggolMenu[i].text.color = new Color(0, 0, 0, 255f);
    }
    public void NextButton(TMP_InputField inputfield)
    {
        if (string.IsNullOrEmpty(inputfield.text))
        {
            Debug.Log("Fill all field");
        }
        else
        {
            
        }
    }
}
