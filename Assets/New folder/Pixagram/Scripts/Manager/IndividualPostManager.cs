using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;

public class IndividualPostManager : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI locationTxt;
    public TextMeshProUGUI post;
    public TextMeshProUGUI reactiontxt;
    public TextMeshProUGUI commentsTxt;
    public InputField addCommentsTxt;

    public Image profileImage;
    public Image postedImage;

    [HideInInspector]
    public Texture2D texture2D;

    public Button profileBtn;
    public Button moreBtn;
    public Button reactionBtn;
    public Button commentsBtn;
    public Button messageBtn;
    public Button reactionListBtn;
    public Button readFullPost;
    public Button commentsListBtn;
    public Button commentPostBtn;

    public GameObject likesText;
    public GameObject CommentsText;
    public GameObject likeImage;
    public GameObject heartImage;
    public GameObject postImage;
    public GameObject likesPrefab;
    public GameObject commentPrefab;

    public List<Like> likes;
    public List<Comment> comments;

    public string postID;

    IndividualLikeManager individualLikeManager;
    IndividualCommentManager individualCommentManager;
    int i;
    bool isLiked;
    string bodyJsonString;
    string urlType;

    private void Start()
    {
        ButtonAction();
        postImage.SetActive(false);
        if (reactiontxt.text == "")
        {
            likesText.SetActive(false);
            isLiked = false;
        }
        if (commentsTxt.text == "")
        {
            CommentsText.SetActive(false);
        }
        heartImage.SetActive(false);
        i = 0;
    }
    public void DoubleTab()
    {
        i++;
        if (i == 2)
        {
            StartCoroutine(DeActivateImage());
        }

    }
    IEnumerator DeActivateImage()
    {
        heartImage.SetActive(true);
        likeImage.SetActive(true);
        i = 0;
        yield return new WaitForSeconds(2f);
        heartImage.SetActive(false);
    }
    void ButtonAction()
    {
        profileBtn.onClick.AddListener(() =>
        {
            Debug.Log("profile button clicked");
        });
        moreBtn.onClick.AddListener(() =>
        {
            Debug.Log("more button clicked");
        });
        reactionBtn.onClick.AddListener(() =>
        {
            Debug.Log("reactionBtn button clicked");
            if (likeImage.activeSelf)
            {
                likeImage.SetActive(false);
            }
            else
            {
                likeImage.SetActive(true);
            }
            urlType = "reaction";
            PostLike postlike = new PostLike();
            postlike.postid = postID;
            bodyJsonString = JsonConvert.SerializeObject(postlike);
            StartCoroutine(RequestUrl(StringResources.postLike));
        });
        commentsBtn.onClick.AddListener(() =>
        {
            Debug.Log("commentsBtn button clicked");
            foreach (var comment in comments)
            {
                DashboardManager.instance.commentScrollingPanel.ForceUpdateRectTransforms();
                GameObject commentItem = Instantiate(commentPrefab, DashboardManager.instance.commentScrollingPanel);
                individualCommentManager = commentItem.GetComponent<IndividualCommentManager>();
                individualCommentManager.message.text = "<b>" + comment.user.userName + "</b> " + comment.message;
            }
            AppManager.instance.pageManager.ShowPage("CommentsPanel");
        });
        messageBtn.onClick.AddListener(() =>
        {
            Debug.Log("messageBtn button clicked");
            MessageBoxController.instance.ShowMessage("Alert", "Coming Soon...");
        });
        reactionListBtn.onClick.AddListener(() =>
        {
            Debug.Log("reactionListBtn button clicked");
            DashboardManager.instance.LikeScrollingPanel.DestoryAllChildImmediate();
            foreach (var like in likes)
            {
                DashboardManager.instance.LikeScrollingPanel.ForceUpdateRectTransforms();
                GameObject likeItem = Instantiate(likesPrefab, DashboardManager.instance.LikeScrollingPanel);
                individualLikeManager = likeItem.GetComponent<IndividualLikeManager>();
                individualLikeManager.profileName.text = like.user.userFullName;
                individualLikeManager.userName.text = like.user.userName;
            }
            AppManager.instance.pageManager.ShowPage("LikesPanel");
        });
        readFullPost.onClick.AddListener(() =>
        {
            Debug.Log("readFullPost button clicked");
        });
        commentsListBtn.onClick.AddListener(() =>
        {
            Debug.Log("commentsListBtn button clicked");
            foreach (var comment in comments)
            {
                DashboardManager.instance.commentScrollingPanel.ForceUpdateRectTransforms();
                GameObject commentItem = Instantiate(commentPrefab, DashboardManager.instance.commentScrollingPanel);
                individualCommentManager = commentItem.GetComponent<IndividualCommentManager>();
                individualCommentManager.message.text = "<b>" + comment.user.userName + "</b> " + comment.message;
            }
            AppManager.instance.pageManager.ShowPage("CommentsPanel");
        });
        commentPostBtn.onClick.AddListener(() =>
        {
            if (commentsTxt.text!=null)
            {
                urlType = "comment";
                PostComment postComment = new PostComment();
                postComment.postid = postID;
                postComment.commentbody = addCommentsTxt.text;
                bodyJsonString = JsonConvert.SerializeObject(postComment);
                StartCoroutine(RequestUrl(StringResources.postComment));
            }
            
        });
    }

    public void SetImage()
    {
        postImage.SetActive(false);
        Texture2D texture = texture2D;
        if (texture == null) return;
        RectTransform rt = postedImage.GetComponent<RectTransform>();
        
        postedImage.sprite = Sprite.Create(texture,
            new Rect(0.0f, 0.0f, texture.width, texture.height),
            new Vector2(0.5f, 0.5f), 100.0f);
        postedImage.preserveAspect = true;
    }
    public void LikeManage()
    {
        if (likes.Count > 0)
        {
            likesText.SetActive(true);
            reactiontxt.text = likes.Count + " Likes";
            
        }
        else
        {
            likesText.SetActive(false);
            reactiontxt.text = "";
        }
    }
    public IEnumerator GetImage(string url)
    {
        /*var request = new UnityWebRequest(StringResources.baseURL+url, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log(request.error);
            Debug.Log("Failure");
        }
        else
        {
            texture2D.LoadImage(request.downloadHandler.data);
            SetImage();
        }*/
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(StringResources.baseURL+"/" + url))
        {
            //request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            Debug.Log(request.url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                // Get downloaded asset bundle
                //texture2D = DownloadHandlerTexture.GetContent(request);

                //texture2D.LoadImage( request.downloadHandler.data);
                //texture2D = request.downloadHandler.data;
                texture2D = null;
                texture2D = ((DownloadHandlerTexture)request.downloadHandler).texture;
                SetImage();
            }
        }

    }

    IEnumerator RequestUrl(string extendUrl)
    {
        string url = StringResources.baseURL + extendUrl;
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + AppManager.instance.bearerToken);
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log(AppManager.instance.bearerToken);
        print(url);
        print(bodyJsonString);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log(request.error);
            Debug.Log("Failure");
        }
        else
        {
            print(request.downloadHandler.text);
            if (urlType == "reaction")
            {
                print(request.downloadHandler.text);
            }
        }
    }
}
