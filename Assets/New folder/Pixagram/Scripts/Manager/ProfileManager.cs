using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager instance;
    
    public GameObject postPrefab;
    public GameObject profileItemPrefab;

    public RectTransform postScrollingPanel;
    public RectTransform postItemScrollingPanel;
    public RectTransform LikeScrollingPanel;
    public RectTransform commentScrollingPanel;

    IndividualPostManager individualPostManager;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        print("sfnjl");
        //ShowLocalFile();
        StartCoroutine(RequestAPI());
    }


    public IEnumerator RequestAPI()
    {
        DashboardRequest dashboardData = new DashboardRequest();
        string url = StringResources.baseURL + StringResources.mypost;


        string bodyJsonString = JsonConvert.SerializeObject(dashboardData);
        var request = new UnityWebRequest(url, "GET");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + AppManager.instance.bearerToken);
        Debug.Log(request.url);
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
            print(request.downloadHandler.text);
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.NullValueHandling = NullValueHandling.Ignore;
            DashboardResponse apiResponse = JsonConvert.DeserializeObject<DashboardResponse>(jsonResponse, jsonSetting);
            postScrollingPanel.ForceUpdateRectTransforms();
            for (int i = 0; i < apiResponse.data.Count; i++)
            {
                GameObject postItem = Instantiate(postPrefab, postScrollingPanel);
                
                individualPostManager = postItem.GetComponent<IndividualPostManager>();
                individualPostManager.postID = apiResponse.data[i].id;
                individualPostManager.nameTxt.text = apiResponse.data[i].userName;
                individualPostManager.locationTxt.text = apiResponse.data[i].location;
                //individualPostManager.profileImage

                StartCoroutine(individualPostManager.GetImage(apiResponse.data[i].medias[0]));

                individualPostManager.post.text = apiResponse.data[i].postBody;
                individualPostManager.reactiontxt.text = apiResponse.data[i].likes.Count != 0 ? apiResponse.data[i].likes.Count + " Likes" : "";
                individualPostManager.commentsTxt.text = apiResponse.data[i].comments.Count == 0 ? "" : "View All " + apiResponse.data[i].comments.Count + " Comments";
                if (apiResponse.data[i].likes.Count > 0)
                {
                    individualPostManager.likes = apiResponse.data[i].likes;
                }
                if (apiResponse.data[i].comments.Count > 0)
                {
                    individualPostManager.comments = apiResponse.data[i].comments;
                }

                GameObject profilePostItem = Instantiate(profileItemPrefab, postItemScrollingPanel);
                Button postButton = profilePostItem.GetComponent<Button>();
                Image postImage = profilePostItem.GetComponent<Image>();
                postButton.onClick.AddListener(() =>
                 {
                     AppManager.instance.pageManager.ShowPage("ProfileDashboard");
                 });
            }
        }
    }

}
