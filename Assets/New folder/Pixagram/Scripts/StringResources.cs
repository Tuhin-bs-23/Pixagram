using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class StringResources
{
    public static string baseURL = "http://172.16.229.250";
    public readonly static string signIn = "/api/Auth/login";
    public readonly static string signUp = "/api/Auth/register";
    public readonly static string userCheck = "/api/User/check";
    public readonly static string createotp = "/api/Auth/createotp";
    public readonly static string verify = "/api/Auth/otp/verify";
    public readonly static string post = "/api/Post/getall";
    public readonly static string mypost = "/api/Post/mypost";
    public readonly static string createPost = "/api/Post/create";
    public readonly static string postLike = "/api/Like/create/postid";
    public readonly static string postComment = "/api/Comment/create";



}
