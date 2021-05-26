using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class FeedManager : MonoBehaviour
{

    public GameObject feedContent;

    public string feedCollect = "";
    [SerializeField]
    private string token = "";
    void Start()
    {
        StartCoroutine(HitAPI());
        Screen.fullScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator HitAPI()
    {
        var www = new UnityWebRequest(feedCollect,"GET");
        www.SetRequestHeader("Authorization", "Bearer " + token);
        //www.uploadHandler = new UploadHandler();
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        print(www.downloadHandler.text);
        JsonDataHandaling(www.downloadHandler.text);
    }
    private FeedData feedData;
    private void JsonDataHandaling(string data)
    {
        feedData = JsonUtility.FromJson<FeedData>(data);
        print("\nname: "+feedData.items.Count);
        print("\nname: "+feedData.items[0].data.likes.Count);
    }
}
