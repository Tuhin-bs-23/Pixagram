using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IndividualCommentManager : MonoBehaviour
{
    public Image profileImage;
    public TextMeshProUGUI message;
    public Button profileButton;
    // Start is called before the first frame update
    void Start()
    {
        profileButton.onClick.AddListener(() =>
        {
            Debug.Log("Profile Button Clicked");
        });
    }
}
