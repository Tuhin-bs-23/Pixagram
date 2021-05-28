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