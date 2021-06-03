using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IndividualLikeManager : MonoBehaviour
{
    public Image profileImage;
    public TextMeshProUGUI profileName;
    public TextMeshProUGUI userName;
    public Button profileButton;
    public GameObject followButton;
    // Start is called before the first frame update
    void Start()
    {
        Button button = followButton.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Debug.Log("Follow Button Clicked");
        });
        profileButton.onClick.AddListener(() =>
        {
            Debug.Log("Profile Button Clicked");
        });
    }
}
