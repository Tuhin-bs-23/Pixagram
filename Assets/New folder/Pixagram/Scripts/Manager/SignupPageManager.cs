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
    public TMP_InputField phoneNumberInput;
    public TMP_InputField emailInput;
    public TMP_InputField otpInput;
    public TMP_InputField nameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField userNameInput;

    public Button idInputButton;
    public Button otpInputButton;
    public Button nameInputButton;
    public Button passwordInputButton;
    public Button dateInputButton;
    public Button userNameInputButton;
    public Button confirmSignUpButton;

    public ToggleMenu[] toggleMenu;
    int idInputOption = 0;
    string responseType;
    string bodyJsonString;
    private void Start()
    {
        ToggledAction(0);
        ButtonInteractable(false);
        ButtonAction();
    }
    public void ButtonInteractable(bool status)
    {
        idInputButton.interactable = status;
        otpInputButton.interactable = status;
        nameInputButton.interactable = status;
        passwordInputButton.interactable = status;
        //dateInputButton.interactable = status;
        userNameInputButton.interactable = status;
    }
    void ButtonAction()
    {
        idInputButton.onClick.AddListener(() =>
        {
            //BodyJsonString("email");

            //Temporary
            AppManager.instance.pageManager.ShowPage("OtpPanel"); ButtonInteractable(false);
        });
        otpInputButton.onClick.AddListener(() =>
        {
            //BodyJsonString("otp");

            //Temporary
            AppManager.instance.pageManager.ShowPage("InputUserNamePanel"); ButtonInteractable(false);
        });
        nameInputButton.onClick.AddListener(() =>
        {
            //save name


            AppManager.instance.pageManager.ShowPage("PasswordInputPanel"); ButtonInteractable(false);
        });
        passwordInputButton.onClick.AddListener(() =>
        {
            //BodyJsonString("signup");

            //Temporary
            AppManager.instance.pageManager.ShowPage("DateInputPanel"); ButtonInteractable(false);
        });
        dateInputButton.onClick.AddListener(() =>
        {
            //save date

            AppManager.instance.pageManager.ShowPage("ConfirmationPanel"); ButtonInteractable(false);
        });
        userNameInputButton.onClick.AddListener(() =>
        {
            //BodyJsonString("username");

            //Temporary
            AppManager.instance.pageManager.ShowPage("ConfirmationPanel"); ButtonInteractable(false);
        });
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
        phoneNumberInput.text = "";
        emailInput.text = "";
        idInputOption = i;
    }    
    void BodyJsonString(string requestObject)
    {
        switch (requestObject)
        {
            case "email":
                responseType = "email";
                SignUpEmailVerifiedRequest email = new SignUpEmailVerifiedRequest
                {
                    email = emailInput.text
                };
                bodyJsonString = JsonConvert.SerializeObject(email);
                StartCoroutine(RequestAPI());
                break;
            case "otp":
                responseType = "otp";
                SignUpOTPVerifiedRequest otp = new SignUpOTPVerifiedRequest();
                otp.otp = otpInput.text;
                bodyJsonString = JsonConvert.SerializeObject(otp);
                StartCoroutine(RequestAPI());
                break;
            case "phone":
                responseType = "phone";
                SignUpPhoneVerifiedRequest phone = new SignUpPhoneVerifiedRequest();
                phone.phone = phoneNumberInput.text;
                bodyJsonString = JsonConvert.SerializeObject(phone);
                StartCoroutine(RequestAPI());
                break;
            case "username":
                responseType = "username";
                SignUpNameVerifiedRequest name = new SignUpNameVerifiedRequest();
                name.username = userNameInput.text;
                bodyJsonString = JsonConvert.SerializeObject(name);
                StartCoroutine(RequestAPI());
                break;
            case "signup":
                responseType = "signup";
                SignUpRequest userData = new SignUpRequest();
                userData.email = PlayerPrefs.HasKey("useremail") ? PlayerPrefs.GetString("useremail") : "";
                userData.phone = PlayerPrefs.HasKey("userphone") ? PlayerPrefs.GetString("userphone") : "";
                userData.username = PlayerPrefs.HasKey("username") ? PlayerPrefs.GetString("username") : "";
                userData.password = passwordInput.text;
                bodyJsonString = JsonConvert.SerializeObject(userData);
                StartCoroutine(RequestAPI());
                break;
            default:
                break;
        }
    }
    public IEnumerator RequestAPI()
    {
        string url = StringResources.baseURL + StringResources.signUp;
        ButtonInteractable(false);
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
            ResponseAPI(request.downloadHandler.text);
        }
    }

    void ResponseAPI(string jsonResponse)
    {
        JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
        jsonSetting.NullValueHandling = NullValueHandling.Ignore;

        SignUpResponse apiResponse = JsonConvert.DeserializeObject<SignUpResponse>(jsonResponse, jsonSetting);
        if (!apiResponse.success)
        {
            MessageBoxController.instance.ShowMessage("", apiResponse.message);
        }
        else
        {
            switch (responseType)
            {
                case "email":
                    PlayerPrefs.SetString("useremail", emailInput.text);
                    AppManager.instance.pageManager.ShowPage("OtpPanel");
                    break;
                case "phone":
                    PlayerPrefs.SetString("userphone", phoneNumberInput.text);
                    AppManager.instance.pageManager.ShowPage("OtpPanel");
                    break;
                case "otp":
                    AppManager.instance.pageManager.ShowPage("InputNamePanel");
                    break;
                case "username":
                    PlayerPrefs.SetString("username", userNameInput.text);
                    AppManager.instance.pageManager.ShowPage("ConfirmationPanel");
                    break;
                case "signup":
                    PlayerPrefs.SetString("username", apiResponse.userName);
                    AppManager.instance.pageManager.ShowPage("DateInputPanel");
                    break;
                default:
                    break;
            }
        }
    }
}
