using System.Collections;
using System.Collections.Generic;


public class SignInResponse
{
    public bool success;
    public int successCode;
    public string data;
    public string message;
    public List<string> errors;
}

public class SignInRequest
{
    public string username;
    public string password;

}

public class SignUpResponse
{
    public bool success;
    public int successCode;
    
    public string message;
    public List<string> errors;
}

public class SignUpVerifiedRequest
{
    public string username;
    public string password;

}
public class DashboardRequest
{
    public string userId;
}
public class DashboardResponse
{
    public bool success;
    public int successCode;
    public List<DashboardPostResponse> post;
    public string message;
    public List<string> errors;
}
public class DashboardPostResponse
{
    public bool success;
    public int successCode;
    public string profileimage;
    public string profilename;
    public string location;
    public string sharedPost;
    public string postBody;
    public string reactionlist;
    public string commentslist;
    public string message;
    public List<string> errors;
}