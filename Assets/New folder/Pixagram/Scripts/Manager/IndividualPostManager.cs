using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class IndividualPostManager : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI locationTxt;
    public TextMeshProUGUI post;
    public TextMeshProUGUI reactiontxt;
    public TextMeshProUGUI commentsTxt;

    public Image profileImage;
    public Image postedImage;

    public Button profileBtn;
    public Button moreBtn;
    public Button reactionBtn;
    public Button commentsBtn;
    public Button messageBtn;
    public Button reactionListBtn;
    public Button readFullPost;
    public Button commentsListBtn;

    

    private void Start()
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
        });
        commentsBtn.onClick.AddListener(() =>
        {
            Debug.Log("commentsBtn button clicked");
        });
        messageBtn.onClick.AddListener(() =>
        {
            Debug.Log("messageBtn button clicked");
            MessageBoxController.instance.ShowMessage("Alert", "Coming Soon...");
        });
        reactionListBtn.onClick.AddListener(() =>
        {
            Debug.Log("reactionListBtn button clicked");
            AppManager.instance.pageManager.ShowPage("LikesPanel");
        });
        readFullPost.onClick.AddListener(() =>
        {
            Debug.Log("readFullPost button clicked");
        });
        commentsListBtn.onClick.AddListener(() =>
        {
            Debug.Log("commentsListBtn button clicked");
            AppManager.instance.pageManager.ShowPage("CommentsPanel");
        });
    }
}
