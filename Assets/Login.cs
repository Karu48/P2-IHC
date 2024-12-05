using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private string URL = "http://localhost:5000";
    enum Status
    {
        username,
        password,
        enter,
        Logged
    }
    
    string username;
    string password;
    Status currentStatus = Status.username;

    public TMP_Text text;
    public InputField input;
    
    public GameObject nextButton;
    public GameObject loginButton;
    public GameObject registerButton;

    public GameObject BasketGameButton;
    public GameObject ShooterGameButton;
    public GameObject BowlingGameButton;
    public Canvas canvas;

    void Start()
    {
        text.text = "Username";
        nextButton.SetActive(true);
        loginButton.SetActive(false);
        registerButton.SetActive(false);
        BasketGameButton.SetActive(false);
        ShooterGameButton.SetActive(false);
        BowlingGameButton.SetActive(false);
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

                loginButton.SetActive(false);
                registerButton.SetActive(false);
            }
            if (registerButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                text.text = "please wait...";
                currentStatus = Status.enter;
                password = input.text;
                input.text = "";
                StartCoroutine(register());

                loginButton.SetActive(false);
                registerButton.SetActive(false);
            }
        }

        if (currentStatus == Status.Logged)
        {
            nextButton.SetActive(false);
            loginButton.SetActive(false);
            registerButton.SetActive(false);
            canvas.gameObject.SetActive(false);

            BasketGameButton.SetActive(true);
            ShooterGameButton.SetActive(true);
            BowlingGameButton.SetActive(true);

            if (BasketGameButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                // change scene
                SceneManager.LoadScene("Basket");
            }

            if (ShooterGameButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                // change scene
                SceneManager.LoadScene("Shooting");
            }

            if (BowlingGameButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                // change scene
                SceneManager.LoadScene("Bowling");
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
                Debug.LogWarning(request.downloadHandler.text);
                StartCoroutine(login());
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
                Debug.LogWarning( request.downloadHandler.text);
                currentStatus = Status.Logged;
            }
        }
    }
}