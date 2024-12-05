using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    public int score = 0;
    float timer = 10;
    public TMP_Text scoreText;
    public TMP_Text timerText;

    public GameObject[] balls;
    public GameObject ReadyButton;
    public GameObject RestartButton;
    public GameObject ExitButton;
    public AudioSource audioSource;

    bool gameOver = false;
    bool ready = false;
    bool ingame = false;

    float readyTimer = 3;

    // Start is called before the first frame update
    void Start()
    {
        timer = 30;
        scoreText.text = "Score: 0";
        foreach (GameObject ball in balls)
        {
            ball.SetActive(false);
        }
        RestartButton.SetActive(false);
        ExitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ingame == false)
        {
            if (ready)
            {
                readyTimer -= Time.deltaTime;
                timerText.text = readyTimer.ToString("F0");
                if (readyTimer <= 0)
                {
                    ingame = true;
                    ready = false;
                    readyTimer = 3;
                }
            }
            else
            {
                if (ReadyButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
                    {
                        ready = true;
                        ReadyButton.SetActive(false);
                    }
            }
        }
        else
        {
            foreach (GameObject ball in balls)
            {
                ball.SetActive(true);
            }

            if (gameOver == false)
            {
                timer -= Time.deltaTime;
                timerText.text = "Time: " + timer.ToString("F2");
                if (timer <= 0)
                {
                    GameOver();
                    gameOver = true;
                    timerText.text = "Game Over";
                    RestartButton.SetActive(true);
                    ExitButton.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject ball in balls)
                {
                    ball.SetActive(false);
                }
            }
        }

        if (RestartButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {      
            SceneManager.LoadScene("Basket");
        } 
                        
        if (ExitButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {										
            SceneManager.LoadScene("Lobby"); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        score++;
        audioSource.Play();
        scoreText.text = "Score: " + score/2;
    }

    void GameOver()
    {
        foreach (GameObject ball in balls)
        {
            ball.SetActive(false);
        }
    }
}
