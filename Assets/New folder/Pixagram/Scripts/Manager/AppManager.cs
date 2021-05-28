using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;
    public string bearerToken;


    public PageManager pageManager;
    private void Awake()
    {
        instance = this;
    }


}
