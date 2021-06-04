using System;
using System.Collections;
using System.Collections.Generic;


public class SignInResponse
{
    public bool success;
    public int successCode;
    public Token data;
    public string message;
    public List<string> errors;
}
public class Token
{
    public string token;
}
public class SignInRequest
{
    public string userid;
    public string password;

}
#region SignUp
public class SignUpResponse
{
    public bool success;
    public int successCode;
    public string userName;
    public string message;
    public List<string> errors;
}
public class SignUpVerifiedResponse
{
    public bool success;
    public int successCode;
    public string message;
    public List<string> errors;
}
public class SignUpEmailVerifiedRequest
{
    public string email;
}
public class SignUpPhoneVerifiedRequest
{
    public string phone;
}
public class SignUpOTPVerifiedRequest
{
    public string otp;
}
public class SignUpNameVerifiedRequest
{
    public string username;
}
public class SignUpRequest
{
    public string email;
    public string fullname;
    //public DateTime dateofbirth;
    public string password;
}
#endregion
#region Dashboard
public class DashboardRequest
{
    public string userId;
}
public class DashboardResponse
{
    public bool success;
    public int successCode;
    public List<DashboardPostResponse> data;
    //public DashboardPostResponse data;
    public string message;
    public List<string> errors;
}
public class DashboardPostResponse
{
    public bool success;
    public int successCode;

    public string id;
    public string profileimage;
    public string userName;
    public string userId;
    public string location;
    public string postBody;
    public List<string> medias;
    public List<Like> likes;
    public List<Comment> comments;

    public string message;
    public List<string> errors;
}
public class Like
{
    public bool success;
    public int successCode;

    public string profileimage;
    public string userName;
    public string userId;
    public string relation;

    public string message;
    public List<string> errors;
}
public class Comment
{
    public bool success;
    public int successCode;

    public string id;
    public string userProfilePic;
    public string userName;
    public string message;

    public List<string> errors;
}
#endregion
public class PostLike
{
    public string postid;
}

public class PostComment
{
    public string postid;
    public string commentbody;
}