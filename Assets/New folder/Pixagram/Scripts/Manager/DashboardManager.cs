using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class DashboardManager : MonoBehaviour
{
    public static DashboardManager instance;
    
    public GameObject postPrefab;

    public RectTransform postScrollingPanel;
    public RectTransform LikeScrollingPanel;
    public RectTransform commentScrollingPanel;

    IndividualPostManager individualPostManager;
    IndividualLikeManager individualLikeManager;
    IndividualCommentManager individualCommentManager;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //ShowLocalFile();
        StartCoroutine(RequestAPI());
    }

    void ShowLocalFile(){
        TextAsset jsonFile = Resources.Load<TextAsset>("response_1622463816890");
        string jsonResponse = jsonFile.text;
        JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
        jsonSetting.NullValueHandling = NullValueHandling.Ignore;
        DashboardResponse apiResponse = JsonConvert.DeserializeObject<DashboardResponse>(jsonResponse, jsonSetting);
        Debug.Log(apiResponse.data.Count);
        for (int i = 0; i < apiResponse.data.Count; i++)
        {
            Debug.Log(apiResponse.data.Count + " in " + i);
            GameObject postItem = Instantiate(postPrefab, postScrollingPanel);
            individualPostManager = postItem.GetComponent<IndividualPostManager>();
            individualPostManager.postID = apiResponse.data[i].id;
            individualPostManager.nameTxt.text = apiResponse.data[i].userName;
            individualPostManager.locationTxt.text = apiResponse.data[i].location;
            //individualPostManager.profileImage
            
            //StartCoroutine(individualPostManager.GetImage(apiResponse.post[i].medias[0]));

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
        }
    }
    
    public IEnumerator RequestAPI()
    {
        DashboardRequest dashboardData = new DashboardRequest();
        string url = StringResources.baseURL + StringResources.post;
        

        string bodyJsonString = JsonConvert.SerializeObject(dashboardData);
        var request = new UnityWebRequest(url, "GET");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer "+AppManager.instance.bearerToken);
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
                individualPostManager.nameTxt.text = apiResponse.data[i].id;
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

            }
        }
    } 
}
