using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageBoxController : MonoBehaviour
{
    public static MessageBoxController instance;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Button responseButton;
    public GameObject rootUI;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        responseButton.onClick.AddListener(() =>
        {
            rootUI.SetActive(false);
        });
    }

    public void ShowMessage(string title, string des = "")
    {
        titleText.text = title;
        descriptionText.text = des;
        rootUI.SetActive(true);
    }

}
