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
    //public GameObject[] posts;

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
    // Start is called before the first frame update
    void Start()
    {
        ShowLocalFile();
        
    }


    void ShowLocalFile(){
        TextAsset jsonFile = Resources.Load<TextAsset>("response_1622463816890");
        string jsonResponse = jsonFile.text;
        JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
        jsonSetting.NullValueHandling = NullValueHandling.Ignore;
        DashboardResponse apiResponse = JsonConvert.DeserializeObject<DashboardResponse>(jsonResponse, jsonSetting);
        Debug.Log(apiResponse.post.Count);
        for (int i = 0; i < apiResponse.post.Count; i++)
        {
            Debug.Log(apiResponse.post.Count + " in " + i);
            GameObject postItem = Instantiate(postPrefab, postScrollingPanel);
            individualPostManager = postItem.GetComponent<IndividualPostManager>();
            individualPostManager.postID = apiResponse.post[i].id;
            individualPostManager.nameTxt.text = apiResponse.post[i].userName;
            individualPostManager.locationTxt.text = apiResponse.post[i].location;
            //individualPostManager.profileImage
            
            //StartCoroutine(individualPostManager.GetImage(apiResponse.post[i].medias[0]));

            individualPostManager.post.text = apiResponse.post[i].postBody;
            individualPostManager.reactiontxt.text = apiResponse.post[i].likes.Count != 0 ? apiResponse.post[i].likes.Count + " Likes" : "";
            individualPostManager.commentsTxt.text = apiResponse.post[i].comments.Count == 0 ? "" : "View All " + apiResponse.post[i].comments.Count + " Comments";
            if (apiResponse.post[i].likes.Count > 0)
            {
                individualPostManager.likes = apiResponse.post[i].likes;                
            }
            if (apiResponse.post[i].comments.Count > 0)
            {
                individualPostManager.comments = apiResponse.post[i].comments;                
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
            DashboardResponse apiResponse = JsonConvert.DeserializeObject<DashboardResponse>(jsonResponse, jsonSetting);
            
            for (int i = 0; i < apiResponse.post.Count; i++)
            {
                GameObject postItem = Instantiate(postPrefab, postScrollingPanel);
                individualPostManager = postItem.GetComponent<IndividualPostManager>();

                individualPostManager.nameTxt.text = apiResponse.post[i].userName;
                individualPostManager.locationTxt.text = apiResponse.post[i].location;
                //individualPostManager.profileImage
                //individualPostManager.texture2D.LoadImage(apiResponse.post[i].medias[0]);
                individualPostManager.post.text = apiResponse.post[i].postBody;
                individualPostManager.reactiontxt.text = apiResponse.post[i].likes.Count == 0 ? "" : apiResponse.post[i].likes.Count+" Likes";
                individualPostManager.commentsTxt.text = apiResponse.post[i].comments.Count == 0 ? "" : "View All " + apiResponse.post[i].comments.Count + " Comments";

            }
        }
    } 
}
