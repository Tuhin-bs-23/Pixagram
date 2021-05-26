using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class FeedData
{
    public List<ItemsData> items;
}
[Serializable]
public class ItemsData
{
    public DataForm data;
}
[Serializable]
public class DataForm
{
    public string userName;
    public string userProfilePic;
    public string sharedMedia;
    public List<likesData> likes;
    public List<CommentsData> comment;
}
[Serializable]
public class likesData
{
    public string userName;
    public string userProfilePic;
}
[Serializable]
public class CommentsData
{
    public string userName;
    public string userProfilePic;
    public string message;
}
[Serializable]
public class Value
{
    public string iv;
}
public class Model : MonoBehaviour
{

}
