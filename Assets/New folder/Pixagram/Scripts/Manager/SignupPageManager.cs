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
public class ToggleMenu
{
    public GameObject toggleItem;
    public TextMeshProUGUI text;
    public Image image;
}
public class SignupPageManager : SerializedMonoBehaviour
{
    public TMP_InputField phoneNumber;
    public TMP_InputField email;

    public ToggleMenu[] toggleMenu;
    int idInputOption;
    private void Start()
    {
        ToggledAction(0);
    }

    public void ToggledAction(int i)
    {
        foreach (var menu in toggleMenu)
        {
            menu.toggleItem.SetActive(false);
            menu.image.color = Color.grey;// new Color(50f, 50f, 50f, 128f);
            menu.text.color = Color.grey;// new Color(50f, 50f, 50f, 128f);
        }
        toggleMenu[i].toggleItem.SetActive(true);
        toggleMenu[i].image.color = new Color(0, 0, 0, 255f);
        toggleMenu[i].text.color = new Color(0, 0, 0, 255f);
        phoneNumber.text = "";
        email.text = "";
        idInputOption = i;
    }
    public void IdInputNextButton()
    {

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
