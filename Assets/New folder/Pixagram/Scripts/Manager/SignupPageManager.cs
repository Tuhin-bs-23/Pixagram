using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Newtonsoft.Json;
using System.Text;

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
    int idInputOption = 0;
    string nextpage;
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
    

    public IEnumerator RequestAPI()
    {
        SignInRequest userData = new SignInRequest();
        string url = StringResources.baseURL + StringResources.signIn;
        //userData.username = usernameInput.text;
        //userData.password = passwordInput.text;

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
            SignUpResponse apiResponse = JsonConvert.DeserializeObject<SignUpResponse>(jsonResponse, jsonSetting);
            //AppManager.instance.bearerToken = apiResponse.data;
            AppManager.instance.pageManager.ShowPage("Dashboard");
        }
    }

    
}
