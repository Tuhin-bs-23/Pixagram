using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class LoginPageManager : MonoBehaviour
{
    public TMP_InputField userName;
    public TMP_InputField password;
    [SerializeField] private string login_Url;

    private string login_Request;
    public string Login_Request
    {
        get
        {
            login_Request = "{'userName': '"+userName+"','password': '"+password+"'";
            login_Request = login_Request.Replace("'","\"");
            return login_Request;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //playerPrefsManager = gameObject.GetComponent<PlayerPrefsManager>();
        //print("dv "+ PlayerPrefsManager.instance.userName);
        userName.text = PlayerPrefsManager.instance.UserName;//PlayerPrefs.GetString("username");
        password.text = PlayerPrefs.GetString("password");
    }

    public void Signin()
    {
        if (string.IsNullOrEmpty(userName.text) || string.IsNullOrEmpty(password.text))
        {
            Debug.Log("Fill all field");
        }
        else
        {
            StartCoroutine(HitAPI(login_Url, Login_Request, "POST"));
        }
    }

    public IEnumerator HitAPI(string url, string apiPerameter, string method)
    {
        var www = new UnityWebRequest(url, method);
        string requetTo = apiPerameter + "}";
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(requetTo);

        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("","");
        yield return www.SendWebRequest();
    }
}
