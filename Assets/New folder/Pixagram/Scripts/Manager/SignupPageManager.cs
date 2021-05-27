using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class SignupPageManager : MonoBehaviour
{
    public TMP_InputField phoneNumber;
    public TMP_InputField email;


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
