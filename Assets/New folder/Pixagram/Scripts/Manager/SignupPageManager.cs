using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class SignupPageManager : MonoBehaviour
{
    public GameObject userPhoneOrEmailPanel;
    public GameObject optPanel;
    public GameObject userNamePanel;
    public GameObject passwordInputPanel;
    public GameObject dateInputPanel;
    public GameObject signUpConfirmationPanel;

    void Start()
    {
        userPhoneOrEmailPanel.SetActive(true);
        optPanel.SetActive(false);
        userNamePanel.SetActive(false);
        passwordInputPanel.SetActive(false);
        dateInputPanel.SetActive(false);
    }
}
