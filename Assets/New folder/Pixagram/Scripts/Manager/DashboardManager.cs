using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class DashboardManager : MonoBehaviour
{
    public GameObject postPrefab;
    public GameObject[] posts;

    public RectTransform postScrollingPanel;
    IndividualPostManager individualPostManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public IEnumerator RequestAPI()
    {
        DashboardRequest dashboardData = new DashboardRequest();
        string url = StringResources.baseURL + StringResources.signIn;
        

        string bodyJsonString = JsonConvert.SerializeObject(dashboardData);
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
            DashboardResponse apiResponse = JsonConvert.DeserializeObject<DashboardResponse>(jsonResponse, jsonSetting);
            
            for (int i = 0; i < apiResponse.post.Count; i++)
            {
                GameObject postItem = Instantiate(postPrefab, postScrollingPanel);
                individualPostManager = postItem.GetComponent<IndividualPostManager>();

                individualPostManager.nameTxt.text = apiResponse.post[i].profilename;
                individualPostManager.locationTxt.text = apiResponse.post[i].location;
                //individualPostManager.profileImage
                //individualPostManager.postedImage
                individualPostManager.post.text = apiResponse.post[i].postBody;
                individualPostManager.reactiontxt.text = apiResponse.post[i].reactionlist == "0" ? "" : apiResponse.post[i].reactionlist+" Likes";
                individualPostManager.commentsTxt.text = apiResponse.post[i].commentslist == "0" ? "" : "View All " + apiResponse.post[i].commentslist + " Comments";

            }
            
        }
    }

    
}
