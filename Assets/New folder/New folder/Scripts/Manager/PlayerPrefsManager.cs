using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager
{
    public string userName;
    private string UserName
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
}
