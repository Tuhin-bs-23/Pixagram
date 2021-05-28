using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System.Text;

public class LoginPageManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public GameObject LoadingPanel;
    public Button signInButton;
    
    
    void Start()
    {
        usernameInput.text = PlayerPrefs.HasKey("username")?PlayerPrefs.GetString("username"):"";
        passwordInput.text = PlayerPrefs.HasKey("password") ? PlayerPrefs.GetString("password") : "";
    }

    public void Signin()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            MessageBoxController.instance.ShowMessage("Please Fill All The Fields");
        }
        else
        {
            //StartCoroutine(RequestAPI());
            AppManager.instance.pageManager.ShowPage("Dashboard");
        }
    }

    public IEnumerator RequestAPI()
    {
        StartLoadingSignInButton(true);
        SignInRequest userData = new SignInRequest();
        string url = StringResources.baseURL + StringResources.signIn;
        userData.username = usernameInput.text;
        userData.password = passwordInput.text;
        
        string bodyJsonString = JsonConvert.SerializeObject(userData);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            string jsonResponse = request.downloadHandler.text;
            SignInRequest apiResponse = JsonConvert.DeserializeObject<SignInRequest>(jsonResponse);
            Debug.Log(request.error);
            Debug.Log("Failure");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.NullValueHandling = NullValueHandling.Ignore;
            SignInResponse apiResponse = JsonConvert.DeserializeObject<SignInResponse>(jsonResponse, jsonSetting);
            AppManager.instance.bearerToken = apiResponse.data;
            AppManager.instance.pageManager.ShowPage("Dashboard");
        }
        StartLoadingSignInButton(false);
    }

    public void StartLoadingSignInButton(bool status)
    {
        signInButton.interactable = !status;
        signInButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(!status);
        LoadingPanel.SetActive(status);
    }

    
}
