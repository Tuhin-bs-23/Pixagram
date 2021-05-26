using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager: MonoBehaviour
{
    public static PlayerPrefsManager instance;

    private void Awake()
    {
        instance = this;
    }
    private string userName;
    public string UserName
    {
        get
        {
            if (!PlayerPrefs.HasKey("username"))
            {
                PlayerPrefs.SetString("username", "");
                userName = "";
                return userName;
            }
            else
            {
                userName = PlayerPrefs.GetString("username");
                return userName;
            }
        }
    }
    private string userPassword;
    public string UserPassword
    {
        get
        {
            if (!PlayerPrefs.HasKey("password"))
            {
                PlayerPrefs.SetString("password", "");
                userPassword = "";
                return userPassword;
            }
            else
            {
                userPassword = PlayerPrefs.GetString("password");
                return userPassword;
            }
        }
    }
}
