using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class IndividualPostManager : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI locationTxt;
    public TextMeshProUGUI post;
    public TextMeshProUGUI reactiontxt;
    public TextMeshProUGUI commentsTxt;

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
    

    private void Start()
    {
        ButtonAction();
        postImage.SetActive(false);
        if (reactiontxt.text == "")
        {
            likesText.SetActive(false);
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

        });
        commentsBtn.onClick.AddListener(() =>
        {
            Debug.Log("commentsBtn button clicked");
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
                individualLikeManager.profileName.text = like.userName;
                individualLikeManager.userName.text = like.userId;
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
                individualCommentManager.message.text = "<b>" + comment.userName + "</b> " + comment.message;
            }
            AppManager.instance.pageManager.ShowPage("CommentsPanel");
        });
    }

    public void SetImage()
    {
        postImage.SetActive(false);
        Texture2D texture = texture2D;
        if (texture == null) return;

        postedImage.sprite = Sprite.Create(texture,
            new Rect(0.0f, 0.0f, texture.width, texture.height),
            new Vector2(0.5f, 0.5f), 100.0f);
        postedImage.preserveAspect = true;
    }

    public IEnumerator GetImage(string url)
    {
        var request = new UnityWebRequest(url, "GET");
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
        }
    }

    IEnumerator RequestUrl(string url)
    {
        var request = new UnityWebRequest(url, "POST");
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
            
        }
    }
}
