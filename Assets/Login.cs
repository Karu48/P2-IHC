using System;
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
    public InputField input;
    
    public GameObject nextButton;
    public GameObject loginButton;
    public GameObject registerButton;

    void Start()
    {
        text.text = "Username";
        nextButton.SetActive(true);
        loginButton.SetActive(false);
        registerButton.SetActive(false);
    }

    void Update()
    {
        if (currentStatus == Status.username)
        {
            if (nextButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                nextButton.SetActive(false);
                loginButton.SetActive(true);
                registerButton.SetActive(true);
            
                text.text = "password";
                currentStatus = Status.password;
                username = input.text;
                input.text = "";
            }
        }

        if (currentStatus == Status.password)
        {
            if (loginButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                text.text = "please wait...";
                currentStatus = Status.enter;
                password = input.text;
                input.text = "";
                StartCoroutine(login());
            }
            if (registerButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                text.text = "please wait...";
                currentStatus = Status.enter;
                password = input.text;
                input.text = "";
                StartCoroutine(register());
            }
        }
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
}