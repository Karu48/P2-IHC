using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    float timer = 0;
    public TMP_Text scoreText;
    public TMP_Text timerText;

    public GameObject[] balls;

    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = 60;
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
            if (timer <= 0)
            {
                GameOver();
                gameOver = true;
                timerText.text = "Time: 0";
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        score++;
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
