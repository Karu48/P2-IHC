using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private string URL = "http://localhost:5000";
    enum Status
    {
        username,
        password,
        enter
    }
    
    string username;
    string password;
    Status currentStatus = Status.username;

    public TMP_Text text;
    public InputField inpt;

    void Start()
    {
        // StartCoroutine(register());
    }

    public IEnumerator register()
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL+"/auth/register", "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}", "application/json"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    public IEnumerator login()
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL+"/auth/login", "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}", "application/json"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.result);
            }
        }
    }

    public void changeState()
    {
        switch (currentStatus)
        {
            case Status.username:
                username = inpt.text;
                inpt.text = "";
                text.text = "Password";
                currentStatus = Status.password;
                break;
            case Status.password:
                password = inpt.text;
                inpt.text = "";
                text.text = "bruh";
                StartCoroutine(login());
                currentStatus = Status.enter;
                break;
            case Status.enter:
                currentStatus = Status.username;
                break;
        }
    }

    public void pressLogin()
    {
        StartCoroutine(login());
    }

    public void pressRegister()
    {
        StartCoroutine(register());
    }
}
